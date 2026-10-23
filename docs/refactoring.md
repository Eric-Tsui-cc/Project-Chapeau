# The rebuild in detail — before & after

Companion to the README. Every snippet below is quoted from this repository, with the file it came from. The original SQL Server schema was never inspected (no database file ships with the course project), so statements about the old system are limited to its code.

## 1. Layering and boundaries

**Old — layering existed, but the UI chose when to respect it.**

The same WinForms form sorts through the service and loads through the DAO:

```csharp
// ChapeauUI/Orders.cs
private void SortAndDisplayOrders(string sortBy)
{
    List<Order> sortedOrders = this.orderService.SortOrders(this.orders, sortBy);  // via service
    DisplayOrders(sortedOrders);
}

private void LoadOrderOverview()
{
    orders = new OrderDao().GetAllOrders();   // bypasses the service layer
    DisplayOrders(orders);
}
```

`OrderDao` is a thin wrapper over raw ADO.NET:

```csharp
// ChapeauDAL/BaseDao.cs (abridged)
conn = new SqlConnection(ConfigurationManager
    .ConnectionStrings["Plebbproject"].ConnectionString);
adapter = new SqlDataAdapter();
...
protected void ExecuteEditTranQuery(string query, SqlParameter[] sqlParameters,
    SqlTransaction sqlTransaction)
```

Parameterised, but per-DAO connections, manual parameter arrays, and a shared mutable `SqlConnection` per DAO instance.

**New — one entry point into the domain.**

The API controllers compose EF Core queries directly and every table-facing route is role-gated; the boundary is enforced by structures (attribute authorization, DTO projections, server-side status rules) rather than by convention. For a codebase of this size controllers carrying their queries keeps indirection low; the layer that must not be bypassed — stock, money, and state rules — has no UI-side alternative.

## 2. Authorization and sign-in

**Old.** `LoginPage` → `ChapeauService.IfCorrectChecker_Bogdan` (note the team-member naming convention) hashed the entered user code with SHA-256 and compared against employees; success opened the next WinForms form. Authorization ended at "which form appeared".

```csharp
// ChapeauService/IfCorrectChecker_Bogdan.cs
using SHA256 sha256 = SHA256.Create();
byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(userCode));
```

**New.** PIN login returns a `HttpOnly`, `SameSite=Strict` authentication cookie with an 8-hour window; every business controller carries `[Authorize(Roles = ...)]`; mutating requests additionally require antiforgery tokens (server-side `AutoValidateAntiforgeryToken` + `X-CSRF-TOKEN` from an in-memory token fetched at `/api/auth/csrf`). The SPA and the guest page never handle secrets.

## 3. The order model

**Old — order and order line merged, no prices, dead bill reference.**

```csharp
// ChapeauModel/Order.cs (abridged)
public class Order
{
    public enum OrderStatus { Done, Preparing, Served, Undefined }
    public int OrderId { get; set; }
    public int EmployeeId { get; set; }
    public OrderStatus Status { get; set; }  // one status for the whole order
    public DateTime OrderTakenTime { get; set; }
    public string Comments { get; set; }
    public int TableId { get; set; }
    public int MenuItemId { get; set; }      // one row per dish
    public int Count { get; set; }
    public int BillId { get; set; }          // never referenced by any flow
}
```

Consequences visible in the old UI: a ListView of flat rows, one status shared by the whole table order, no way to bill at the price the guest saw, no checkout implementation.

**New — Order / OrderItem / Bill.**

```
src/Chapeau.Core/Entities/
    Order.cs      Table, responsible employee, created time, IsPaid
    OrderItem.cs  MenuItem, Count, Comment, Status, UnitPrice (captured at order time)
    Bill.cs       OrderId, PaymentMethod, Amount, Tip, PaymentTime, AdjustmentReason
    Table.cs      physical table, status derived from orders
    MenuItem.cs   name, category, price, Stock
```

Now each dish line has its own life cycle, the bill sums `UnitPrice * Count`, and price changes on the menu cannot rewrite history.

## 4. Status handling: free picker → role-owned state machine

**Old.** The shared order list exposed a status combo box; whatever the user picked became the new status:

```csharp
// ChapeauUI/Orders.cs
string newStatus = comboBox1.SelectedItem.ToString();
order.Status = Order.StringToStatus(newStatus);
```

**New.** Transitions are validated on the server per station. The kitchen route accepts only `Running → Preparing` and `Preparing → Prepared` (409 otherwise); the bar has its own subset; serving is a waiter action. The customer order flow creates items in `Running`; the kitchen and bar only ever see items that belong to their categories (`_kitchenCategories` / `_barCategories` in `KitchenController` / `BarController`).

## 5. Stock

**Old.** Menu items were outside the order flow — nothing decremented stock.

**New.** Stock is transactional at both entry points:

- waiter adds an item → row update `item.Stock -= request.Count` inside the same save as the order item (`OrdersController`);
- guest submits a first order → all requested items are collected (`requiredCounts`), validated and decremented in one pass (`CustomerOrdersController`); consumed stock drives `IsAvailable` (`Stock > 0`) for the public menu.

## 6. Checkout

**Old.** `Order.BillId` appeared on the model with no implementation anywhere in UI, Service or DAL.

**New.** `POST /api/bill` (`BillController`, waiter-only):

```csharp
if (order.Items.Any(item => item.Status != OrderStatus.Served))
    return Conflict("Items must be served");
...
subtotal = subtotal + item.UnitPrice * item.Count;
...
Bill bill = new Bill { ..., Amount = request.OverrideAmount ?? subtotal, Tip = request.Tip, ... };
order.IsPaid = true;
order.Table.Status = TableStatus.Free;
_dbContext.Bills.Add(bill);
await _dbContext.SaveChangesAsync();   // bill + paid + table release in one atomic save
```

Guards along the way: unknown order (404), empty order (400), already paid (409), override amount and tip validated non-negative with ≤ 2 decimals, payment method must be a defined enum. The client additionally distinguishes `submitted` / `rejected` / `uncertain` so a dropped connection produces "ask your server to check" instead of a false success.

## 7. What the course version never had

- Public guest ordering (`/menu`, basket, table-number submission with batch stock validation, 409s for tables open without staff).
- The menu assistant: its grounding data (`data/menu-details.json`) is tracked once and consumed by both the API prompt and the SPA; recommendations are validated against the menu server-side.
- Docker Compose deployment (PostgreSQL + API + Nginx), demo mode for offline evaluation, and the `Chapeau.Migrations.Postgres` project.

## 8. Kept as-is, deliberately

- Controllers hold EF queries directly (no repository layer): the indirection would buy nothing at this scale; the enforced boundaries are roles, DTOs and status rules.
- A 4-digit PIN instead of credentials: the threat model is a restaurant LAN, not the public internet (see README "Scope & limitations").
- State on code review: the assistant's request plumbing was AI-generated and reviewed; guardrails and business rules are hand-written.
