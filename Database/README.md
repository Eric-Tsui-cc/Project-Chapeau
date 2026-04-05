# Project Chapeau - 数据库设置指南

## 数据库结构（反向工程自代码）

根据代码中所有 DAO 层的 SQL 语句，推导出以下 6 张表：

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

## 快速启动（3 步）

### 1. 安装 SQL Server

选择以下任一方式：

| 方式 | 适合 | 下载 |
|------|------|------|
| **SQL Server LocalDB** | 最简单，Visual Studio 自带 | 已内置 |
| **SQL Server Express** | 完整本地服务器 | https://go.microsoft.com/fwlink/?linkid=2216019 |
| **Azure SQL** | 云端部署 | Azure Portal |

### 2. 运行建库脚本

```powershell
# 方式 A: 使用 sqlcmd（LocalDB）
sqlcmd -S "(LocalDB)\MSSQLLocalDB" -i "Database\CreateDatabase.sql"

# 方式 B: 使用 SQL Server Management Studio (SSMS)
# 打开 SSMS → 连接到服务器 → 文件 → 打开 → 选择 CreateDatabase.sql → 执行
```

### 3. 配置连接字符串

编辑 `ChapeauUI\App.config`，选择对应环境的连接字符串：

**LocalDB（推荐开发用）：**
```xml
<add name="RestuarantProjectGroup4"
     connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\RestuarantProjectGroup4.mdf;Integrated Security=True;Connect Timeout=30"
     providerName="System.Data.SqlClient" />
```

**SQL Server Express：**
```xml
<add name="RestuarantProjectGroup4"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=RestuarantProjectGroup4;Integrated Security=True;TrustServerCertificate=True"
     providerName="System.Data.SqlClient" />
```

### 4. 运行程序

```powershell
cd "My Branch"
dotnet run --project ChapeauUI
```

## 默认登录账号

| 角色 | 密码 | 用途 |
|------|------|------|
| Waiter | `waiter1` | 服务员 - 餐桌概览、点餐、结账 |
| Chef | `chef1` | 厨师 - 厨房视图 |
| Bartender | `bartender1` | 调酒师 - 厨房视图（饮品） |

> ⚠️ 密码使用 SHA256 哈希存储。如需修改密码，请重新计算 SHA256 并更新 EMPLOYEE 表的 UserCode 字段。

## 表结构详情

### EMPLOYEE
| 列 | 类型 | 说明 |
|---|------|------|
| EmployeeId | INT (PK, 自增) | 员工ID |
| UserCode | NVARCHAR(256) | SHA256 哈希密码 |
| Role | NVARCHAR(20) | Waiter / Chef / Bartender |
| Status | NVARCHAR(20) | Active / Inactive |
| FirstName | NVARCHAR(50) | 名 |
| LastName | NVARCHAR(50) | 姓 |

### TABLE
| 列 | 类型 | 说明 |
|---|------|------|
| TableId | INT (PK, 自增) | 餐桌ID |
| Capacity | INT | 容纳人数 |
| Status | NVARCHAR(20) | Free / Occupied |

### MENU_ITEM
| 列 | 类型 | 说明 |
|---|------|------|
| MenuItemId | INT (PK, 自增) | 菜品ID |
| Name | NVARCHAR(100) | 菜名 |
| Category | NVARCHAR(20) | Mains/Starters/Entremet/Desserts/Beers/Wines/Spirit/CoffeeTea |
| Card | NVARCHAR(20) | Lunch/Drinks/Dinner |
| Price | DECIMAL(10,2) | 价格 |
| Stock | INT | 库存 |

### ORDER
| 列 | 类型 | 说明 |
|---|------|------|
| OrderId | INT (PK, 自增) | 订单ID |
| TableId | INT (FK → TABLE) | 关联餐桌 |
| EmployeeId | INT (FK → EMPLOYEE) | 下单服务员 |
| Status | NVARCHAR(20) | Running/Preparing/Prepared/Served |
| PaymentStatus | INT | 0=未付, 1=已付 |

### ORDER_ITEM
| 列 | 类型 | 说明 |
|---|------|------|
| OrderItemId | INT (PK, 自增) | 订单项ID |
| OrderId | INT (FK → ORDER) | 关联订单 |
| MenuItemId | INT (FK → MENU_ITEM) | 关联菜品 |
| Count | INT | 数量 |
| Status | NVARCHAR(20) | Available/Outofstock |
| OrderTime | DATETIME | 下单时间 |
| Comment | NVARCHAR(255) | 备注 |

### BILL
| 列 | 类型 | 说明 |
|---|------|------|
| BillId | INT (PK, 自增) | 账单ID |
| OrderId | INT (FK → ORDER) | 关联订单 |
| Amount | DECIMAL(10,2) | 金额 |
| Tip | DECIMAL(10,2) | 小费 |
| PaymentMethod | NVARCHAR(20) | Credit/Debit/Cash |
| Date | NVARCHAR(50) | 日期 |
| Time | NVARCHAR(50) | 时间 |
| Feedback | NVARCHAR(500) | 客户反馈 |

## 种子数据

脚本自动插入：
- **10 张餐桌**（容量 2-8 人）
- **4 名员工**（2 服务员 + 1 厨师 + 1 调酒师）
- **39 道菜品**（午餐 12 道 + 晚餐 12 道 + 饮品 15 道）
