# Project Chapeau - Restaurant Management System

A Windows Forms restaurant management application built with C# .NET 6.0 and SQL Server.

---

## Part 1 — Application

### Features

- **Login System** — SHA256-hashed authentication with role-based routing (Waiter / Chef / Bartender)
- **Table Overview** — Visual table status display (Free/Occupied) with color-coded buttons
- **Order Management** — Create orders by selecting menu items, categories, and cards (Lunch/Dinner/Drinks)
- **Kitchen View** — Real-time order queue for kitchen staff with wait time tracking
- **Billing & Payment** — Calculate totals with VAT, tips, and multiple payment methods
- **Table Detail** — View active orders per table, track wait times, mark orders as served

### Architecture

```
ChapeauUI/          → Windows Forms presentation layer
ChapeauService/     → Business logic layer
ChapeauDAL/         → Data access layer (ADO.NET)
ChapeauModel/       → Domain models and enums
Database/           → SQL Server schema and seed scripts
```

### Tech Stack

- **Framework**: .NET 6.0 (Windows Forms)
- **Database**: SQL Server (LocalDB / Express / Azure)
- **Data Access**: ADO.NET with parameterized queries
- **Authentication**: SHA256 password hashing

### Quick Start

#### 1. Set up the database

```powershell
# Using SQL Server LocalDB
sqlcmd -S "(LocalDB)\MSSQLLocalDB" -i "Database\CreateDatabase.sql"
```

Or open `Database/CreateDatabase.sql` in SQL Server Management Studio (SSMS) and execute.

#### 2. Configure connection string

Edit `ChapeauUI/App.config` to match your SQL Server setup. The default uses LocalDB.

#### 3. Build and run

```powershell
dotnet restore "Project Chapeau.sln"
dotnet build "Project Chapeau.sln"
dotnet run --project ChapeauUI
```

#### Default Login Credentials

| Role | Password | Access |
|------|----------|--------|
| Waiter | `waiter1` | Table overview, ordering, billing |
| Chef | `chef1` | Kitchen view |
| Bartender | `bartender1` | Kitchen view (drinks) |

> ⚠️ Passwords are stored as SHA256 hashes. To change a password, recompute the SHA256 hash and update the `UserCode` field in the `EMPLOYEE` table.

### Project Structure

| Layer | Project | Description |
|-------|---------|-------------|
| UI | ChapeauUI | WinForms — LoginPage, ChapeauUI, MakeOrderPage, BillForm, KitchenViewPage, TableInfoForm |
| Service | ChapeauService | Business logic — OrderService, OverviewService, PaymentService, KitchenBarService |
| DAL | ChapeauDAL | Data access — OrderDao, TableDao, MenuItemDao, EmployeeDao, OrderItemDao, BillDao |
| Model | ChapeauModel | Domain entities — Order, Table, Employee, MenuItem, OrderItem, Bill |

---

## Part 2 — Database

### Schema Overview

6 tables: `EMPLOYEE`, `TABLE`, `MENU_ITEM`, `ORDER`, `ORDER_ITEM`, `BILL`

```
┌─────────────┐     ┌──────────────┐     ┌─────────────┐
│   EMPLOYEE  │     │    TABLE     │     │  MENU_ITEM  │
├─────────────┤     ├──────────────┤     ├─────────────┤
│ EmployeeId  │     │ TableId (PK) │     │ MenuItemId  │
│ UserCode    │     │ Capacity     │     │ Name        │
│ Role        │     │ Status       │     │ Category    │
│ Status      │     └──────┬───────┘     │ Card        │
│ FirstName   │            │             │ Price       │
│ LastName    │            │             │ Stock       │
└──────┬───────┘            │             └──────┬──────┘
       │                    │                    │
       │          ┌─────────┴─────────┐          │
       │          │      ORDER        │          │
       │          ├───────────────────┤          │
       └─────────►│ EmployeeId (FK)   │          │
                  │ TableId (FK)      │          │
       ┌─────────►│ OrderId (PK)      │◄─────────┘
       │          │ Status            │
       │          │ PaymentStatus     │
       │          └─────────┬─────────┘
       │                    │
       │          ┌─────────┴─────────┐
       │          │    ORDER_ITEM     │
       │          ├───────────────────┤
       │          │ OrderItemId (PK)  │
       └─────────►│ OrderId (FK)      │
                  │ MenuItemId (FK)   │
                  │ Count             │
                  │ Status            │
                  │ OrderTime         │
                  │ Comment           │
                  └───────────────────┘

       ┌─────────────────────────────┐
       │          BILL               │
       ├─────────────────────────────┤
       │ BillId (PK)                 │
       │ OrderId (FK) → ORDER        │
       │ Amount                      │
       │ Tip                         │
       │ PaymentMethod               │
       │ Date                        │
       │ Time                        │
       │ Feedback                    │
       └─────────────────────────────┘
```

### Table Details

#### EMPLOYEE
| Column | Type | Description |
|--------|------|-------------|
| EmployeeId | INT (PK, Identity) | Employee ID |
| UserCode | NVARCHAR(256) | SHA256-hashed password |
| Role | NVARCHAR(20) | Waiter / Chef / Bartender |
| Status | NVARCHAR(20) | Active / Inactive |
| FirstName | NVARCHAR(50) | First name |
| LastName | NVARCHAR(50) | Last name |

#### TABLE
| Column | Type | Description |
|--------|------|-------------|
| TableId | INT (PK, Identity) | Table ID |
| Capacity | INT | Seating capacity |
| Status | NVARCHAR(20) | Free / Occupied |

#### MENU_ITEM
| Column | Type | Description |
|--------|------|-------------|
| MenuItemId | INT (PK, Identity) | Menu item ID |
| Name | NVARCHAR(100) | Item name |
| Category | NVARCHAR(20) | Mains / Starters / Entremet / Desserts / Beers / Wines / Spirit / CoffeeTea |
| Card | NVARCHAR(20) | Lunch / Drinks / Dinner |
| Price | DECIMAL(10,2) | Price |
| Stock | INT | Stock quantity |

#### ORDER
| Column | Type | Description |
|--------|------|-------------|
| OrderId | INT (PK, Identity) | Order ID |
| TableId | INT (FK → TABLE) | Associated table |
| EmployeeId | INT (FK → EMPLOYEE) | Waiter who placed the order |
| Status | NVARCHAR(20) | Running / Preparing / Prepared / Served |
| PaymentStatus | INT | 0 = unpaid, 1 = paid |

#### ORDER_ITEM
| Column | Type | Description |
|--------|------|-------------|
| OrderItemId | INT (PK, Identity) | Order item ID |
| OrderId | INT (FK → ORDER) | Associated order |
| MenuItemId | INT (FK → MENU_ITEM) | Associated menu item |
| Count | INT | Quantity |
| Status | NVARCHAR(20) | Available / Outofstock |
| OrderTime | DATETIME | Time the order was placed |
| Comment | NVARCHAR(255) | Special instructions |

#### BILL
| Column | Type | Description |
|--------|------|-------------|
| BillId | INT (PK, Identity) | Bill ID |
| OrderId | INT (FK → ORDER) | Associated order |
| Amount | DECIMAL(10,2) | Total amount |
| Tip | DECIMAL(10,2) | Tip amount |
| PaymentMethod | NVARCHAR(20) | Credit / Debit / Cash |
| Date | NVARCHAR(50) | Payment date |
| Time | NVARCHAR(50) | Payment time |
| Feedback | NVARCHAR(500) | Customer feedback |

### Seed Data

The `CreateDatabase.sql` script automatically inserts:
- **10 tables** (capacity 2–8 seats)
- **4 employees** (2 waiters + 1 chef + 1 bartender)
- **39 menu items** (12 lunch + 12 dinner + 15 drinks)

### Connection String Options

**LocalDB (recommended for development):**
```xml
<add name="RestuarantProjectGroup4"
     connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\RestuarantProjectGroup4.mdf;Integrated Security=True;Connect Timeout=30"
     providerName="System.Data.SqlClient" />
```

**SQL Server Express:**
```xml
<add name="RestuarantProjectGroup4"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=RestuarantProjectGroup4;Integrated Security=True;TrustServerCertificate=True"
     providerName="System.Data.SqlClient" />
```

---

## License

This project was developed as a group assignment for educational purposes.
