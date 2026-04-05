# Project Chapeau - Restaurant Management System

A Windows Forms restaurant management application built with C# .NET 6.0 and SQL Server.

## Features

- **Login System** — SHA256-hashed authentication with role-based routing (Waiter / Chef / Bartender)
- **Table Overview** — Visual table status display (Free/Occupied) with color-coded buttons
- **Order Management** — Create orders by selecting menu items, categories, and cards (Lunch/Dinner/Drinks)
- **Kitchen View** — Real-time order queue for kitchen staff with wait time tracking
- **Billing & Payment** — Calculate totals with VAT, tips, and multiple payment methods
- **Table Detail** — View active orders per table, track wait times, mark orders as served

## Architecture

```
ChapeauUI/          → Windows Forms presentation layer
ChapeauService/     → Business logic layer
ChapeauDAL/         → Data access layer (ADO.NET)
ChapeauModel/       → Domain models and enums
Database/           → SQL Server schema and seed scripts
```

## Tech Stack

- **Framework**: .NET 6.0 (Windows Forms)
- **Database**: SQL Server (LocalDB / Express / Azure)
- **Data Access**: ADO.NET with parameterized queries
- **Authentication**: SHA256 password hashing

## Quick Start

### 1. Set up the database

```powershell
# Using SQL Server LocalDB
sqlcmd -S "(LocalDB)\MSSQLLocalDB" -i "Database\CreateDatabase.sql"
```

Or open `Database/CreateDatabase.sql` in SQL Server Management Studio and execute.

### 2. Configure connection string

Edit `ChapeauUI/App.config` to match your SQL Server setup. The default uses LocalDB.

### 3. Build and run

```powershell
dotnet restore "Project Chapeau.sln"
dotnet build "Project Chapeau.sln"
dotnet run --project ChapeauUI
```

### Default Login Credentials

| Role | Password |
|------|----------|
| Waiter | `waiter1` |
| Chef | `chef1` |
| Bartender | `bartender1` |

## Database Schema

6 tables: `EMPLOYEE`, `TABLE`, `MENU_ITEM`, `ORDER`, `ORDER_ITEM`, `BILL`

See `Database/README.md` for full schema documentation and ER diagram.

## Project Structure

| Layer | Project | Description |
|-------|---------|-------------|
| UI | ChapeauUI | WinForms — LoginPage, ChapeauUI, MakeOrderPage, BillForm, KitchenViewPage, TableInfoForm |
| Service | ChapeauService | Business logic — OrderService, OverviewService, PaymentService, KitchenBarService |
| DAL | ChapeauDAL | Data access — OrderDao, TableDao, MenuItemDao, EmployeeDao, OrderItemDao, BillDao |
| Model | ChapeauModel | Domain entities — Order, Table, Employee, MenuItem, OrderItem, Bill |

## License

This project was developed as a group assignment for educational purposes.
