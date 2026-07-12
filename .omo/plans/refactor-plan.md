# Project Chapeau — 重构计划

> 日期: 2026-07-07 · 环境: macOS (Apple Silicon) · 目标: 作品集级 Restaurant Management System

---

## 策略

**旧项目** WinForms + ADO.NET + SQL Server → **新架构** .NET Web API + React + PostgreSQL

重构不是重写。每一段旧代码都应该在新架构中找到对应的位置。每次只改一层，改完能跑。

### 核心原则

1. **增量交付** — 每阶段产出可展示、可部署的成果
2. **开发零配置** — 本地用 SQLite，不需要装数据库服务
3. **部署全在 Oracle** — API + PostgreSQL + 前端全在 Oracle ARM 免费实例上
4. **零成本基础设施** — 开发/部署全部使用免费 tiers
5. **旧代码为蓝本** — 实体结构、业务逻辑从旧代码移植，不重新设计

### 技术选型

| 层 | 技术 | 原因 |
|---|---|---|
| 运行时 | .NET 8 SDK (LTS) | 当前只有 6/7，需安装；LTS 稳定，作品集认可度高 |
| 本地数据库 | **SQLite** (开发用) | 零安装零配置，`dotnet add package Microsoft.EntityFrameworkCore.Sqlite` 即可 |
| 生产数据库 | **PostgreSQL 16** (Oracle VM 上 Docker 跑) | 企业级，符合澳洲市场需求 |
| ORM | EF Core 8 | 替换 ADO.NET，自动迁移 |
| 架构 | Clean Architecture (Core → Infrastructure → API) | 单向依赖，可测试 |
| 认证 | JWT Bearer | 前后端分离标准 |
| 前端 | React 19 + Vite + TypeScript + Tailwind CSS | 生态最成熟 |
| UI 组件 | shadcn/ui | 开箱即用，不付费 |
| 实时通信 | SignalR | 厨房看板用 |
| AI | OpenAI SDK / Gemini API | 按量计费，demo 用量几块钱 |
| 部署 | **Oracle Cloud Always Free (ARM VM)** | 4 核 24GB 免费，一台跑所有 |
| CI/CD | GitHub Actions | 免费 |
| Web 服务器 | **Nginx** | 反代 API + 托管前端静态文件 |

---

## 项目结构

```
Project-Chapeau/
├── src/
│   ├── Chapeau.Core/               # 实体、枚举、接口 (classlib)
│   ├── Chapeau.Infrastructure/    # EF Core DbContext、Repository (classlib)
│   └── Chapeau.Api/               # REST API 控制器、JWT (webapi)
├── frontend/                       # React 前端
├── Database/                       # 迁移脚本、种子数据 (保留参考)
├── deploy/                         # Docker Compose + Nginx 配置
├── ChapeauModel/                   # 旧项目 — 保留不动
├── ChapeauDAL/                     # 旧项目 — 保留不动
├── ChapeauService/                 # 旧项目 — 保留不动
├── ChapeauUI/                      # 旧项目 — 保留不动
└── ProjectChapeau.sln              # 新解决方案
```

> 旧项目文件全部保留，新代码放在 `src/` 下，便于对比。

---

## 本地开发 vs 生产部署

| | 本地开发 (macOS) | 生产部署 (Oracle ARM VM) |
|---|---|---|
| 数据库 | SQLite (`chapeau_dev.db`) | PostgreSQL 16 (Docker) |
| .NET API | `dotnet run` 直接跑 | Docker 容器 |
| 前端 | `npm run dev` (热更新) | Nginx 托管静态文件 |
| 域名 | `localhost:5001` | `your-domain.com` |

> EF Core 的代码在两种数据库下完全一样，只需要在 `appsettings.Development.json` 和 `appsettings.Production.json` 里切换连接字符串和数据库提供者。迁移也是同一套。

---

## Phase 0 — 开发环境

### 0.1 安装 .NET 8 SDK

```bash
# 下载 ARM64 安装包
# 访问 https://dotnet.microsoft.com/download/dotnet/8.0
# 选择 macOS ARM64 版本安装

# 验证
dotnet --list-sdks
# 应该看到 8.0.x 出现

# 设置项目使用 .NET 8
# 在项目根目录执行:
dotnet new globaljson --sdk-version 8.0.100
```

### 0.2 安装 Node.js (前端用)

```bash
# 检查是否已有
node --version

# 如果没有或版本低于 18:
brew install node@20
```

### 0.3 注册 Oracle Cloud 免费账号

- 访问 https://www.oracle.com/cloud/free/
- 需要信用卡验证（仅验证身份，免费资源不扣费）
- 注册后创建 ARM VM:
  - 镜像: Ubuntu 22.04 / Oracle Linux 8
  - 配置: 选择 4 核 OCPU + 24GB 内存
  - 注意: 有些区域 ARM 实例缺货，选 `ap-sydney-1`（到澳洲延迟最低）或 `ap-osaka-1`（库存足）
- 记下公网 IP，配置 SSH 密钥

> **提示**: 注册 Oracle Cloud 后还要申请增加 ARM 实例配额（默认 0），这是免费计划的一步，跟着官方的 `Create VM` 流程走就会提示你。

> **Oracle 这部分你完全可以放在 Phase 5 部署时才做。** 前 4 个 Phase 不依赖它。可以先注册，VM 等有空了再开。

---

## Phase 1 — Domain 层 (Chapeau.Core)

### 目标

将 `ChapeauModel/` 中的 6 个实体 + 7 个枚举移植到 `Chapeau.Core`，同时做三处改进。

### 步骤

#### 1.1 创建解决方案骨架

```bash
cd /Users/eric/RiderProjects/Project-Chapeau
mkdir src
cd src

dotnet new classlib -n Chapeau.Core --framework net8.0
dotnet new classlib -n Chapeau.Infrastructure --framework net8.0
dotnet new webapi -n Chapeau.Api --framework net8.0 --use-controllers

cd ..
dotnet new sln -n ProjectChapeau
dotnet sln add src/Chapeau.Core
dotnet sln add src/Chapeau.Infrastructure
dotnet sln add src/Chapeau.Api

dotnet add src/Chapeau.Infrastructure reference src/Chapeau.Core
dotnet add src/Chapeau.Api reference src/Chapeau.Infrastructure
```

#### 1.2 创建 BaseEntity 基类

`src/Chapeau.Core/BaseEntity.cs`:

```csharp
namespace Chapeau.Core;

public abstract class BaseEntity
{
    public int Id { get; set; }
}
```

#### 1.3 统一枚举命名

旧命名 → 新命名:

| 旧枚举 | 新命名 | 值不变 |
|--------|--------|--------|
| `StatusOfTable` | `TableStatus` | Free, Occupied |
| `StatusOfOrder` | `OrderStatus` | Running, Preparing, Prepared, Served |
| `StatusOfOrderitem` | `OrderItemStatus` | OutOfStock, Available |
| `EmployeeRole` | 不变 (已合理) | Waiter, Chef, Bartender |
| `EmployeeStatus` | 不变 | Active, Inactive |
| `Category` | `MenuCategory` | Mains, Starters, ... |
| `Card` | 不变 | Lunch, Drinks, Dinner |
| `PaymentMethod` | 不变 | Credit, Debit, Cash |

> `StatusOfOrderitem` → `OrderItemStatus`，同时注意 `Outofstock` → `OutOfStock` (PascalCase 规范)。

#### 1.4 移植实体 (从 ChapeauModel 复制并修改)

每个实体都继承 `BaseEntity`，导航属性用外键 ID + 可空导航属性。

**改动对比** (`Order.cs` 为例):

| 旧代码 | 新代码 |
|--------|--------|
| `int OrderId` (自增主键) | `Id` (从 BaseEntity 继承) |
| `Employee Employee` (直接导航) | `int EmployeeId` + `Employee? Employee` |
| `StatusOfOrder Status` | `OrderStatus Status` (枚举已重命名) |
| `int PaymentStatus` (0/1 魔法数字) | `bool IsPaid` (布尔语义明确) |
| `List<OrderItem> items` (小写开头) | `List<OrderItem> Items` (PascalCase) |
| 构造函数写死参数 | 去掉，用 `{ }` 初始化 |

**实体关系图**:

```
┌────────────┐     ┌───────────┐     ┌──────────────┐
│  Employee   │     │   Table   │     │   MenuItem   │
│ (Id, Code,  │     │ (Id,      │     │ (Id, Name,   │
│  Role, ...) │     │  Capacity,│     │  Category,   │
└──────┬──────┘     │  Status)  │     │  Price,...)  │
       │            └─────┬─────┘     └──────┬───────┘
       │                  │                   │
       └────────┬─────────┘                  │
                │                            │
        ┌───────┴────────┐                  │
        │     Order       │                 │
        │ (Id, TableId,   │                 │
        │  EmployeeId,    │                 │
        │  Status,IsPaid) │                 │
        └───────┬────────┘                  │
                │                           │
        ┌───────┴────────┐                 │
        │   OrderItem     ├─────────────────┘
        │ (Id, OrderId,   │
        │  MenuItemId,    │
        │  Count, Status, │
        │  OrderTime,     │
        │  Comment)       │
        └────────────────┘
        
┌──────────────────────┐
│        Bill          │
│ (Id, OrderId,        │
│  Amount, Tip,        │
│  PaymentMethod,      │
│  Date, Time,         │
│  Feedback)           │
└──────────────────────┘
```

#### 1.5 验证 —— 编译通过

```bash
dotnet build src/Chapeau.Core
```

**做完后你应该有:**

```
src/Chapeau.Core/
├── BaseEntity.cs
├── Chapeau.Core.csproj
├── Entities/
│   ├── Bill.cs
│   ├── Employee.cs
│   ├── MenuItem.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   └── Table.cs
└── Enums/
    ├── Card.cs
    ├── EmployeeRole.cs
    ├── EmployeeStatus.cs
    ├── MenuCategory.cs
    ├── OrderItemStatus.cs
    ├── OrderStatus.cs
    ├── PaymentMethod.cs
    └── TableStatus.cs
```

---

## Phase 2 — Infrastructure 层 (Chapeau.Infrastructure)

### 目标

搭建 EF Core DbContext，支持 **本地 SQLite + 生产 PostgreSQL** 双模式。

### 步骤

#### 2.1 添加 NuGet 包

```bash
cd src/Chapeau.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore --version 8.*
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.*
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.*
```

> 同时装了 Npgsql（PostgreSQL 驱动）和 Sqlite 驱动。DbContext 代码对两者通用，只需在 Api 层切换提供者。

#### 2.2 配置 DbContext

`src/Chapeau.Infrastructure/Data/ChapeauDbContext.cs`:

```csharp
namespace Chapeau.Infrastructure.Data;

public class ChapeauDbContext : DbContext
{
    public ChapeauDbContext(DbContextOptions<ChapeauDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Bill> Bills => Set<Bill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 表名映射（沿用旧数据库大写风格，便于对比）
        modelBuilder.Entity<Employee>().ToTable("EMPLOYEE");
        modelBuilder.Entity<Table>().ToTable("TABLE");
        modelBuilder.Entity<MenuItem>().ToTable("MENU_ITEM");
        modelBuilder.Entity<Order>().ToTable("ORDER");
        modelBuilder.Entity<OrderItem>().ToTable("ORDER_ITEM");
        modelBuilder.Entity<Bill>().ToTable("BILL");

        // 主键
        modelBuilder.Entity<Employee>().HasKey(e => e.Id);
        modelBuilder.Entity<Table>().HasKey(e => e.Id);
        // ...其余类似

        // 外键关系
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Employee)
            .WithMany()
            .HasForeignKey(o => o.EmployeeId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Table)
            .WithMany()
            .HasForeignKey(o => o.TableId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany()
            .HasForeignKey(oi => oi.MenuItemId);

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Order)
            .WithMany()
            .HasForeignKey(b => b.OrderId);

        // 枚举存为字符串
        modelBuilder.Entity<Employee>()
            .Property(e => e.Role)
            .HasConversion<string>();
        // ...其余枚举类似

        // 价格精度
        modelBuilder.Entity<MenuItem>()
            .Property(m => m.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Bill>()
            .Property(b => b.Amount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Bill>()
            .Property(b => b.Tip)
            .HasPrecision(10, 2);

        // 种子数据 — 从旧 CreateDatabase.sql 移植
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // 10 张桌子
        modelBuilder.Entity<Table>().HasData(
            new Table { Id = 1, Capacity = 2, Status = TableStatus.Free },
            new Table { Id = 2, Capacity = 2, Status = TableStatus.Free },
            // ...容量 4, 6, 8 的桌子
        );

        // 4 个员工
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, UserCode = "waiter1_hash_here", Role = EmployeeRole.Waiter, Status = EmployeeStatus.Active, FirstName = "Alice", LastName = "Wang" },
            // ...
        );

        // 39 个菜单项 — 按旧的 INSERT 语句逐条核对
    }
}
```

> **参考**: 打开 `Database/CreateDatabase.sql`，把里面的 INSERT INTO 语句逐条转为 C# 的 HasData 调用。注意密码 hash 需要和旧数据一致（否则登录不了）。

#### 2.3 连接字符串配置

`src/Chapeau.Api/appsettings.Development.json` (本地开发):

```json
{
  "ConnectionStrings": {
    "ChapeauDb": "Data Source=chapeau_dev.db"
  },
  "DatabaseProvider": "Sqlite"
}
```

`src/Chapeau.Api/appsettings.Production.json` (部署时用):

```json
{
  "ConnectionStrings": {
    "ChapeauDb": "Host=localhost;Database=chapeau;Username=postgres;Password=你的密码"
  },
  "DatabaseProvider": "PostgreSQL"
}
```

`src/Chapeau.Api/Program.cs`:

```csharp
var provider = builder.Configuration.GetValue<string>("DatabaseProvider");
var connectionString = builder.Configuration.GetConnectionString("ChapeauDb");

builder.Services.AddDbContext<ChapeauDbContext>(options =>
{
    if (provider == "PostgreSQL")
        options.UseNpgsql(connectionString);
    else
        options.UseSqlite(connectionString);
});
```

#### 2.4 创建迁移

```bash
# 安装 dotnet-ef 工具（如果还没有）
dotnet tool install --global dotnet-ef

# 创建初始迁移
cd src/Chapeau.Api
dotnet ef migrations add InitialCreate --project ../Chapeau.Infrastructure

# 应用迁移到本地 SQLite
dotnet ef database update
```

#### 2.5 验证 —— 本地数据存在

```bash
# 直接查看 SQLite 数据库
sqlite3 src/Chapeau.Api/chapeau_dev.db "SELECT COUNT(*) FROM MENU_ITEM;"
# 应该返回 39

# 或者启动 API 看看 Swagger
dotnet run --project src/Chapeau.Api
# 浏览器打开 https://localhost:5001/swagger
```

---

## Phase 3 — API 层 (Chapeau.Api)

### 目标

RESTful API 端点 + JWT 认证 + Swagger 文档。

### 步骤

#### 3.1 添加 NuGet 包

```bash
cd src/Chapeau.Api
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.*
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.*
```

#### 3.2 配置 JWT 认证

`src/Chapeau.Api/appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-super-secret-key-at-least-32-characters-long",
    "Issuer": "Chapeau.Api",
    "Audience": "Chapeau.Frontend",
    "ExpireMinutes": 120
  }
}
```

`Program.cs`:

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
```

#### 3.3 Controller 列表

| 控制器 | 端点 | 对应旧代码 | 认证 |
|--------|------|-----------|------|
| `AuthController` | `POST /api/auth/login` | EmployeeDao.VerifyCredentials | ❌ 公开 |
| `TableController` | `GET /api/tables` | TableDao.GetAllTables | ✅ |
| `MenuItemController` | `GET /api/menu-items` | MenuItemDao.GetMenuItems | ✅ |
| `OrderController` | `GET/POST /api/orders` | OrderDao, OrderService | ✅ |
| `OrderItemController` | `PUT /api/orders/{id}/items` | OrderItemDao | ✅ |
| `BillController` | `POST /api/bills` | BillDao, PaymentService | ✅ |

#### 3.4 登录端点核心逻辑

`POST /api/auth/login`:

```csharp
// 1. 接收 { userCode: string, password: string }
// 2. 对 password 做 SHA256
// 3. 查数据库：SELECT * FROM EMPLOYEE WHERE UserCode = @hashedPassword
// 4. 匹配成功 → 生成 JWT（payload 含 EmployeeId, Role）
// 5. 匹配失败 → 401
```

> 旧项目的 `UserCode` 字段存的是密码 hash，不是用户名。登录时用户输入 `waiter1` / `waiter1`，后端对 `waiter1` 做 SHA256 后去匹配 `UserCode`。这个设计虽然怪，但在重构第一阶段保持兼容，部署后再考虑升级到 BCrypt。

#### 3.5 验证

```bash
cd src/Chapeau.Api
dotnet run

# 另一个终端里测试
# 登录
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"userCode":"waiter1","password":"waiter1"}'

# 获取菜单（替换 token）
curl -X GET https://localhost:5001/api/menu-items \
  -H "Authorization: Bearer <token>"
```

---

## Phase 4 — React 前端

### 目标

服务员端 UI（替代旧 WinForms）+ 顾客自助点餐端。

### 步骤

#### 4.1 创建前端项目

```bash
npm create vite@latest frontend -- --template react-ts
cd frontend
npm install
npm install tailwindcss @tailwindcss/vite
npm install lucide-react
npm install react-router-dom
```

配置 Tailwind + shadcn/ui:

```bash
npx shadcn@latest init
npx shadcn@latest add button card table dialog input select
```

#### 4.2 页面规划

| 路由 | 页面 | 对应旧 WinForms |
|------|------|----------------|
| `/login` | 登录页 | LoginPage |
| `/tables` | 餐桌概览（颜色编码） | ChapeauUI 主界面 |
| `/tables/:id/order` | 点餐（三栏布局） | MakeOrderPage |
| `/tables/:id/bill` | 结账 | BillForm |
| `/kitchen` | 厨房看板 | KitchenViewPage |
| `/orders/:id` | 订单详情 | TableInfoForm |

#### 4.3 API 调用封装

`src/api/client.ts`:

```typescript
const API_BASE = import.meta.env.VITE_API_URL || '/api';
// 开发时 .env.development 设为 http://localhost:5001/api
// 生产时用相对路径 /api，Nginx 反代到后端

async function request<T>(path: string, options?: RequestInit): Promise<T> {
  const token = localStorage.getItem('token');
  const res = await fetch(`${API_BASE}${path}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...options?.headers,
    },
  });
  if (!res.ok) throw new Error(await res.text());
  return res.json();
}
```

#### 4.4 构建前端

```bash
cd frontend
npm run build
# 产物在 frontend/dist/，后面 Nginx 直接托管这个目录
```

---

## Phase 5 — 部署到 Oracle ARM VM

### 目标

一台 Oracle VM 跑所有东西: Nginx → 前端静态文件 + 反代 API → .NET 容器 → PostgreSQL。

### 架构

```
                          Oracle ARM VM (4核 24GB)
                          ┌──────────────────────────────┐
                          │   Nginx (端口 80/443)         │
                          │   ├── / → frontend/dist       │
                          │   └── /api/* → localhost:8080 │
                          │                              │
                          │   Docker Compose              │
                          │   ├── chapeau-api (端口 8080) │
                          │   └── postgres (端口 5432)    │
                          └──────────────────────────────┘
                                   ▲
                                   │
用户 ──► your-domain.com ──────────┘
```

### 步骤

#### 5.1 Oracle VM 初始化

```bash
# SSH 登录
ssh -i ~/.ssh/oracle_key ubuntu@<你的公网IP>

# 安装 Docker
sudo apt update && sudo apt install docker.io docker-compose-v2 -y
sudo usermod -aG docker $USER  # 退出重新登录生效

# 验证
docker --version && docker compose version
```

#### 5.2 准备部署文件

`deploy/docker-compose.yml`:

```yaml
services:
  db:
    image: postgres:16-alpine
    restart: always
    environment:
      POSTGRES_DB: chapeau
      POSTGRES_PASSWORD: chapeau_prod_password
    volumes:
      - pgdata:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U postgres"]
      interval: 5s

  api:
    build:
      context: ../src/Chapeau.Api
      dockerfile: ../../deploy/Dockerfile
    restart: always
    ports:
      - "127.0.0.1:8080:8080"
    environment:
      ASPNETCORE_ENVIRONMENT: Production
      ASPNETCORE_URLS: http://+:8080
      ConnectionStrings__ChapeauDb: "Host=db;Database=chapeau;Username=postgres;Password=chapeau_prod_password"
      DatabaseProvider: PostgreSQL
      Jwt__Key: "换成随机生成的32位以上密钥"
    depends_on:
      db:
        condition: service_healthy

volumes:
  pgdata:
```

`deploy/Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Chapeau.Api.dll"]
```

`deploy/nginx.conf`:

```nginx
server {
    listen 80;
    server_name your-domain.com;

    # 前端静态文件
    root /var/www/frontend;
    index index.html;

    # SPA 路由
    location / {
        try_files $uri $uri/ /index.html;
    }

    # API 反代
    location /api/ {
        proxy_pass http://localhost:8080/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

#### 5.3 GitHub Actions 自动部署

`.github/workflows/deploy.yml`:

```yaml
name: Deploy

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Build frontend
        run: |
          cd frontend
          npm ci
          npm run build

      - name: Deploy to Oracle VM
        run: |
          # scp 前端文件到服务器
          scp -r frontend/dist/ ubuntu@${{ secrets.ORACLE_IP }}:/var/www/frontend/
          # 重启 API 容器
          ssh ubuntu@${{ secrets.ORACLE_IP }} "cd /home/ubuntu/app && docker compose pull && docker compose up -d"
```

> GitHub Actions 需要配置 Secrets: `ORACLE_IP`、`SSH_PRIVATE_KEY`。

---

## Phase 6 — 亮点功能

### 6.1 SignalR 厨房看板

- 当 waiter 提交订单，厨房端实时看到新订单
- ASP.NET Core SignalR Hub + React `@microsoft/signalr`
- 替代旧 WinForms `KitchenViewPage` 的定时刷新

### 6.2 AI 聊天助手 (自然语言点餐)

- OpenAI GPT-4o-mini 或 Gemini 2.5 Flash
- Function Calling: AI 调用 `placeOrder`, `getMenuItems`, `getTableStatus`
- 前端: 聊天气泡 UI
- 保护: 服务器端 API key, 速率限制, 消费上限

> **作品集价值**: AI integration 是简历最大的加分项。

---

## 时间线建议

```
Phase 0  环境搭建          1 小时       → 装 .NET 8 + Node.js + 注册 Oracle
Phase 1  Domain 层         2-3 小时     → 最基础, 决定了所有后续代码质量
Phase 2  Infrastructure    2-3 小时     → EF Core 双数据库配置 + 种子数据
Phase 3  API               3-4 小时     → 此时已经有了可运行的 backend
Phase 4  前端              4-8 小时     → 最花时间, 但也是作品集门面
Phase 5  部署             2-3 小时     → Docker + Nginx + CI/CD
Phase 6  亮点功能          4-8 小时     → 加分项, 可延迟/可缩减
```

> **不要等全部做完再投简历。** Phase 3 跑起来后就可以更新简历说 "重构了一个全栈 Web API"，放 GitHub 链接。Phase 4 后加 "React 前端"，Phase 5 后加 "部署上线链接"。

---

## 常见问题

### Q: 本地用 SQLite，部署用 PostgreSQL，迁移会不会冲突？
A: 不会。EF Core 迁移文件是数据库无关的。你本地开发时 `dotnet ef database update` 会在 SQLite 上建表，部署时 Production 环境在 PostgreSQL 上跑相同的迁移，自动建一样的表。

### Q: Oracle Cloud 信用卡验证安全吗？
A: 安全。Oracle 只做身份验证，不会扣费。只要不手动升级到付费账号，永远是免费的。

### Q: Oracle ARM 实例开不到怎么办？
A: 多试几个区域。悉尼（`ap-sydney-1`）库存紧，大阪（`ap-osaka-1`）和首尔（`ap-seoul-1`）库存足。你也可以先等几个小时再试，Oracle 会补充库存。

### Q: 旧代码里的拼写错误要修复吗？
A: 修复。`Restuarant` → `Restaurant`, `Outofstock` → `OutOfStock`。这是重构的一部分，面试官会看到。

### Q: 没有测试怎么办？
A: Phase 0-3 不需要测试。如果时间充裕，可以在 Phase 3 末尾给核心业务逻辑（计算账单、验证订单）加 xUnit 测试。

---

## 参考

- 旧项目数据库 Schema: `Database/CreateDatabase.sql`
- 旧项目实体: `ChapeauModel/*.cs`
- 旧项目数据访问: `ChapeauDAL/*Dao.cs`
- 旧项目业务逻辑: `ChapeauService/*Service.cs`
- 旧项目 UI 参考: `ChapeauUI/*.cs` (布局和交互逻辑)
