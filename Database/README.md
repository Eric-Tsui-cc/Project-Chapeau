# Project Chapeau - Database Setup Guide

## Database Schema (Reverse-Engineered from Code)

By analyzing all SQL statements across the DAO layer, the following 6 tables were derived:

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

## Quick Start

### 1. Install SQL Server

Choose one of the following options:

| Option | Best For | Download |
|--------|----------|----------|
| **SQL Server LocalDB** | Easiest, included with Visual Studio | Built-in |
| **SQL Server Express** | Full local server | https://go.microsoft.com/fwlink/?linkid=2216019 |
| **Azure SQL** | Cloud deployment | Azure Portal |

### 2. Run the Database Script

```powershell
# Option A: Using sqlcmd (LocalDB)
sqlcmd -S "(LocalDB)\MSSQLLocalDB" -i "Database\CreateDatabase.sql"

# Option B: Using SQL Server Management Studio (SSMS)
# Open SSMS → Connect to server → File → Open → Select CreateDatabase.sql → Execute
```

### 3. Configure the Connection String

Edit `ChapeauUI\App.config` and select the connection string that matches your environment:

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

### 4. Run the Application

```powershell
dotnet run --project ChapeauUI
```

## Default Login Credentials

| Role | Password | Access |
|------|----------|--------|
| Waiter | `waiter1` | Table overview, ordering, billing |
| Chef | `chef1` | Kitchen view |
| Bartender | `bartender1` | Kitchen view (drinks) |

> ⚠️ Passwords are stored as SHA256 hashes. To change a password, recompute the SHA256 hash and update the `UserCode` field in the `EMPLOYEE` table.

## Table Schema Details

### EMPLOYEE
| Column | Type | Description |
|--------|------|-------------|
| EmployeeId | INT (PK, Identity) | Employee ID |
| UserCode | NVARCHAR(256) | SHA256-hashed password |
| Role | NVARCHAR(20) | Waiter / Chef / Bartender |
| Status | NVARCHAR(20) | Active / Inactive |
| FirstName | NVARCHAR(50) | First name |
| LastName | NVARCHAR(50) | Last name |

### TABLE
| Column | Type | Description |
|--------|------|-------------|
| TableId | INT (PK, Identity) | Table ID |
| Capacity | INT | Seating capacity |
| Status | NVARCHAR(20) | Free / Occupied |

### MENU_ITEM
| Column | Type | Description |
|--------|------|-------------|
| MenuItemId | INT (PK, Identity) | Menu item ID |
| Name | NVARCHAR(100) | Item name |
| Category | NVARCHAR(20) | Mains / Starters / Entremet / Desserts / Beers / Wines / Spirit / CoffeeTea |
| Card | NVARCHAR(20) | Lunch / Drinks / Dinner |
| Price | DECIMAL(10,2) | Price |
| Stock | INT | Stock quantity |

### ORDER
| Column | Type | Description |
|--------|------|-------------|
| OrderId | INT (PK, Identity) | Order ID |
| TableId | INT (FK → TABLE) | Associated table |
| EmployeeId | INT (FK → EMPLOYEE) | Waiter who placed the order |
| Status | NVARCHAR(20) | Running / Preparing / Prepared / Served |
| PaymentStatus | INT | 0 = unpaid, 1 = paid |

### ORDER_ITEM
| Column | Type | Description |
|--------|------|-------------|
| OrderItemId | INT (PK, Identity) | Order item ID |
| OrderId | INT (FK → ORDER) | Associated order |
| MenuItemId | INT (FK → MENU_ITEM) | Associated menu item |
| Count | INT | Quantity |
| Status | NVARCHAR(20) | Available / Outofstock |
| OrderTime | DATETIME | Time the order was placed |
| Comment | NVARCHAR(255) | Special instructions |

### BILL
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

## Seed Data

The script automatically inserts:
- **10 tables** (capacity 2–8 seats)
- **4 employees** (2 waiters + 1 chef + 1 bartender)
- **39 menu items** (12 lunch + 12 dinner + 15 drinks)
