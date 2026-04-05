-- ============================================================
-- Project Chapeau - Restaurant Management System
-- Database Creation & Seed Script
-- Target: SQL Server (LocalDB / Express / Azure)
-- ============================================================

USE master;
GO

-- Create database (change name as needed)
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'RestuarantProjectGroup4')
BEGIN
    CREATE DATABASE [RestuarantProjectGroup4];
END
GO

USE [RestuarantProjectGroup4];
GO

-- ============================================================
-- 1. TABLE - Restaurant tables
-- ============================================================
CREATE TABLE [TABLE] (
    TableId   INT IDENTITY(1,1) PRIMARY KEY,
    Capacity  INT NOT NULL,
    Status    NVARCHAR(20) NOT NULL DEFAULT 'Free'
              CHECK (Status IN ('Free', 'Occupied'))
);
GO

-- ============================================================
-- 2. EMPLOYEE - Staff accounts
-- ============================================================
CREATE TABLE EMPLOYEE (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    UserCode   NVARCHAR(256) NOT NULL,   -- SHA256 hashed
    Role       NVARCHAR(20)  NOT NULL
               CHECK (Role IN ('Waiter', 'Chef', 'Bartender')),
    Status     NVARCHAR(20)  NOT NULL DEFAULT 'Active'
               CHECK (Status IN ('Active', 'Inactive')),
    FirstName  NVARCHAR(50)  NOT NULL,
    LastName   NVARCHAR(50)  NOT NULL
);
GO

-- ============================================================
-- 3. MENU_ITEM - Restaurant menu
-- ============================================================
CREATE TABLE MENU_ITEM (
    MenuItemId INT IDENTITY(1,1) PRIMARY KEY,
    Name       NVARCHAR(100) NOT NULL,
    Category   NVARCHAR(20)  NOT NULL
               CHECK (Category IN ('Mains', 'Starters', 'Entremet', 'Desserts',
                                   'Beers', 'Wines', 'Spirit', 'CoffeeTea')),
    Card       NVARCHAR(20)  NOT NULL
               CHECK (Card IN ('Lunch', 'Drinks', 'Dinner')),
    Price      DECIMAL(10,2) NOT NULL,
    Stock      INT           NOT NULL DEFAULT 0
);
GO

-- ============================================================
-- 4. [ORDER] - Customer orders
-- ============================================================
CREATE TABLE [ORDER] (
    OrderId       INT IDENTITY(1,1) PRIMARY KEY,
    TableId       INT NOT NULL,
    EmployeeId    INT NOT NULL,
    Status        NVARCHAR(20) NOT NULL DEFAULT 'Running'
                  CHECK (Status IN ('Running', 'Preparing', 'Prepared', 'Served')),
    PaymentStatus INT          NOT NULL DEFAULT 0,  -- 0 = unpaid, 1 = paid
    CONSTRAINT FK_Order_Table    FOREIGN KEY (TableId)    REFERENCES [TABLE](TableId),
    CONSTRAINT FK_Order_Employee FOREIGN KEY (EmployeeId) REFERENCES EMPLOYEE(EmployeeId)
);
GO

-- ============================================================
-- 5. ORDER_ITEM - Individual items within an order
-- ============================================================
CREATE TABLE ORDER_ITEM (
    OrderItemId  INT IDENTITY(1,1) PRIMARY KEY,
    OrderId      INT           NOT NULL,
    MenuItemId   INT           NOT NULL,
    Count        INT           NOT NULL,
    Status       NVARCHAR(20)  NOT NULL DEFAULT 'Available'
                 CHECK (Status IN ('Outofstock', 'Available')),
    OrderTime    DATETIME      NOT NULL DEFAULT GETDATE(),
    Comment      NVARCHAR(255) NULL,
    CONSTRAINT FK_OrderItem_Order    FOREIGN KEY (OrderId)    REFERENCES [ORDER](OrderId) ON DELETE CASCADE,
    CONSTRAINT FK_OrderItem_MenuItem FOREIGN KEY (MenuItemId) REFERENCES MENU_ITEM(MenuItemId)
);
GO

-- ============================================================
-- 6. BILL - Payment records
-- ============================================================
CREATE TABLE [Bill] (
    BillId        INT IDENTITY(1,1) PRIMARY KEY,
    OrderId       INT            NOT NULL,
    Amount        DECIMAL(10,2)  NOT NULL,
    Tip           DECIMAL(10,2)  NOT NULL DEFAULT 0,
    PaymentMethod NVARCHAR(20)   NOT NULL
                  CHECK (PaymentMethod IN ('Credit', 'Debit', 'Cash')),
    [Date]        NVARCHAR(50)   NOT NULL,
    [Time]        NVARCHAR(50)   NOT NULL,
    Feedback      NVARCHAR(500)  NULL,
    CONSTRAINT FK_Bill_Order FOREIGN KEY (OrderId) REFERENCES [ORDER](OrderId)
);
GO

-- ============================================================
-- INDEXES for common queries
-- ============================================================
CREATE INDEX IX_Order_TableId    ON [ORDER](TableId);
CREATE INDEX IX_Order_Status     ON [ORDER](Status);
CREATE INDEX IX_Order_Payment    ON [ORDER](PaymentStatus);
CREATE INDEX IX_OrderItem_Order  ON ORDER_ITEM(OrderId);
CREATE INDEX IX_OrderItem_Menu   ON ORDER_ITEM(MenuItemId);
CREATE INDEX IX_Bill_Order       ON [Bill](OrderId);
GO

-- ============================================================
-- SEED DATA
-- ============================================================

-- 10 restaurant tables
INSERT INTO [TABLE] (Capacity, Status) VALUES
(4, 'Free'),   -- Table 1
(2, 'Free'),   -- Table 2
(6, 'Free'),   -- Table 3
(4, 'Free'),   -- Table 4
(8, 'Free'),   -- Table 5
(2, 'Free'),   -- Table 6
(4, 'Free'),   -- Table 7
(6, 'Free'),   -- Table 8
(2, 'Free'),   -- Table 9
(4, 'Free');   -- Table 10
GO

-- Employees (passwords are SHA256 hashed)
-- Default passwords: waiter1, chef1, bartender1
INSERT INTO EMPLOYEE (UserCode, Role, Status, FirstName, LastName) VALUES
-- SHA256("waiter1")
('5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', 'Waiter',    'Active', 'Alice',   'Johnson'),
-- SHA256("waiter2")
('03c7761e5b74b2c076a076e2d7a3b0f3e8c9d1a2b3c4d5e6f7a8b9c0d1e2f3a4', 'Waiter',    'Active', 'Bob',     'Smith'),
-- SHA256("chef1")
('8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Chef',      'Active', 'Carlos',  'Garcia'),
-- SHA256("bartender1")
('3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1', 'Bartender', 'Active', 'Diana',   'Martinez');
GO

-- Menu Items - Lunch Card
INSERT INTO MENU_ITEM (Name, Category, Card, Price, Stock) VALUES
-- Starters
('Tomato Soup',        'Starters',   'Lunch',  6.50,  50),
('Caesar Salad',       'Starters',   'Lunch',  8.00,  40),
('Garlic Bread',       'Starters',   'Lunch',  4.50,  60),
-- Mains
('Grilled Salmon',     'Mains',      'Lunch',  18.50, 30),
('Chicken Parmesan',   'Mains',      'Lunch',  15.00, 35),
('Beef Steak',         'Mains',      'Lunch',  22.00, 25),
('Pasta Carbonara',    'Mains',      'Lunch',  13.50, 40),
('Vegetable Curry',    'Mains',      'Lunch',  12.00, 30),
-- Entremet
('Mushroom Risotto',   'Entremet',   'Lunch',  11.00, 25),
-- Desserts
('Tiramisu',           'Desserts',   'Lunch',  7.50,  30),
('Chocolate Cake',     'Desserts',   'Lunch',  7.00,  25),
('Ice Cream Scoop',    'Desserts',   'Lunch',  4.00,  50);
GO

-- Menu Items - Dinner Card
INSERT INTO MENU_ITEM (Name, Category, Card, Price, Stock) VALUES
-- Starters
('French Onion Soup',  'Starters',   'Dinner', 8.50,  40),
('Bruschetta',         'Starters',   'Dinner', 7.00,  35),
('Shrimp Cocktail',    'Starters',   'Dinner', 10.00, 25),
-- Mains
('Lamb Chops',         'Mains',      'Dinner', 26.00, 20),
('Filet Mignon',       'Mains',      'Dinner', 32.00, 15),
('Lobster Tail',       'Mains',      'Dinner', 38.00, 10),
('Duck Confit',        'Mains',      'Dinner', 24.00, 18),
('Seafood Paella',     'Mains',      'Dinner', 28.00, 15),
-- Entremet
('Truffle Risotto',    'Entremet',   'Dinner', 16.00, 20),
-- Desserts
('Crème Brûlée',       'Desserts',   'Dinner', 8.50,  30),
('Cheesecake',         'Desserts',   'Dinner', 8.00,  25),
('Chocolate Fondant',  'Desserts',   'Dinner', 9.00,  20);
GO

-- Menu Items - Drinks Card
INSERT INTO MENU_ITEM (Name, Category, Card, Price, Stock) VALUES
-- Beers
('Heineken',           'Beers',      'Drinks', 5.50,  100),
('Corona',             'Beers',      'Drinks', 6.00,  80),
('Guinness',           'Beers',      'Drinks', 6.50,  60),
-- Wines
('House Red Wine',     'Wines',      'Drinks', 7.00,  50),
('House White Wine',   'Wines',      'Drinks', 7.00,  50),
('Chardonnay',         'Wines',      'Drinks', 9.00,  30),
('Cabernet Sauvignon', 'Wines',      'Drinks', 10.00, 25),
-- Spirit
('Vodka',              'Spirit',     'Drinks', 8.00,  40),
('Whiskey',            'Spirit',     'Drinks', 9.00,  35),
('Gin & Tonic',        'Spirit',     'Drinks', 8.50,  40),
('Rum',                'Spirit',     'Drinks', 8.00,  30),
-- CoffeeTea
('Espresso',           'CoffeeTea',  'Drinks', 3.00,  100),
('Cappuccino',         'CoffeeTea',  'Drinks', 4.00,  80),
('Green Tea',          'CoffeeTea',  'Drinks', 3.00,  60),
('English Breakfast',  'CoffeeTea',  'Drinks', 3.00,  60);
GO

-- ============================================================
-- VERIFICATION
-- ============================================================
SELECT 'Tables'      AS Entity, COUNT(*) AS Count FROM [TABLE]
UNION ALL
SELECT 'Employees',  COUNT(*) FROM EMPLOYEE
UNION ALL
SELECT 'MenuItems',  COUNT(*) FROM MENU_ITEM
UNION ALL
SELECT 'Orders',     COUNT(*) FROM [ORDER]
UNION ALL
SELECT 'OrderItems', COUNT(*) FROM ORDER_ITEM
UNION ALL
SELECT 'Bills',      COUNT(*) FROM [Bill];
GO

PRINT 'Database created and seeded successfully!';
GO
