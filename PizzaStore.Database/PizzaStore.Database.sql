USE Master;
GO

CREATE DATABASE PizzaStoreDb;
GO

USE PizzaStoreDb;
GO

CREATE SCHEMA Pizza;
GO

CREATE SCHEMA Orders;
GO

CREATE SCHEMA Users;
GO

CREATE SCHEMA Store;
GO

-- When recreating all the tables:
-- load Crust, Size, Toppings, Users, Store,
-- then Orders,
-- then Pizza,
-- then PizzaTopping

CREATE TABLE Pizza.Pizza
(
    PizzaId INT NOT NULL IDENTITY(1, 1),
    OrderId INT NOT NULL,
    CrustId TINYINT NULL,
    SizeId TINYINT NULL,
    [Name] NVARCHAR(250) NOT NULL,
    Price MONEY NOT NULL,
    CONSTRAINT PK_PizzaId PRIMARY KEY (PizzaId),
    CONSTRAINT FK_OrderId FOREIGN KEY (OrderId) REFERENCES Orders.Orders(OrderId),
    CONSTRAINT FK_CrustId FOREIGN KEY (CrustId) REFERENCES Pizza.Crust(CrustId),
    CONSTRAINT FK_SizeId FOREIGN KEY (SizeId) REFERENCES Pizza.Size(SizeId),
);

CREATE TABLE Pizza.Crust
(
    CrustId TINYINT IDENTITY(1, 1),
    [Name] NVARCHAR(100) NOT NULL,
    Price MONEY NOT NULL,
    CONSTRAINT PK_CrustId PRIMARY KEY (CrustId),
);

CREATE TABLE Pizza.Size
(
    SizeId TINYINT IDENTITY(1, 1),
    [Name] NVARCHAR(100) NOT NULL,
    Price MONEY NOT NULL,
    CONSTRAINT PK_Size_SizeId PRIMARY KEY (SizeId)
);

CREATE TABLE Pizza.Toppings
(
    ToppingId SMALLINT IDENTITY(1, 1),
    [Name] NVARCHAR(250) NOT NULL,
    Price MONEY NOT NULL,
    CONSTRAINT PK_ToppingId PRIMARY KEY (ToppingId)
);

CREATE TABLE Pizza.PizzaTopping
(
    PizzaToppingId SMALLINT NOT NULL IDENTITY(1, 1),
    PizzaId INT NOT NULL,
    ToppingId SMALLINT NOT NULL,
    CONSTRAINT PK_PizzaToppingId PRIMARY KEY (PizzaToppingId),
    CONSTRAINT FK_PizzaId FOREIGN KEY (PizzaId) REFERENCES Pizza.Pizza(PizzaId),
    CONSTRAINT FK_ToppingId FOREIGN KEY (ToppingId) REFERENCES Pizza.Toppings(ToppingId)
);

CREATE TABLE Orders.Orders
(
    OrderId INT NOT NULL IDENTITY(1, 1),
    UserSubmittedId INT NOT NULL,
    StoreSubmittedId INT NOT NULL,
    Price MONEY NOT NULL,
    PurchaseDate DATETIME2(0) NOT NULL DEFAULT GetDate(),
    CONSTRAINT PK_OrderId PRIMARY KEY (OrderId),
    CONSTRAINT FK_UserSubmittedId FOREIGN KEY (UserSubmittedId) REFERENCES Users.Users(UserId),
    CONSTRAINT FK_StoreSubmittedId FOREIGN KEY (StoreSubmittedId) REFERENCES Store.Store(StoreId)
);

CREATE TABLE Users.Users
(
    UserId INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_UserId PRIMARY KEY (UserId)
);

CREATE TABLE Store.Store
(
    StoreId INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_StoreId PRIMARY KEY (StoreId)
);
GO

-- Sizes
INSERT INTO Pizza.Size
    ([Name], Price)
VALUES('S', 5);

INSERT INTO Pizza.Size
    ([Name], Price)
VALUES('M', 7);

INSERT INTO Pizza.Size
    ([Name], Price)
VALUES('L', 10);


-- Crusts
INSERT INTO Pizza.Crust
    ([Name], Price)
VALUES('Thin', 5);

INSERT INTO Pizza.Crust
    ([Name], Price)
VALUES('Thick', 7);

INSERT INTO Pizza.Crust
    ([Name], Price)
VALUES('Garlic', 7);

INSERT INTO Pizza.Crust
    ([Name], Price)
VALUES('Garlic Stuffed', 10);


-- Toppings
INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Sauce', 0.25);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Cheese', 0.25);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Pepperoni', 0.50);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Sausage', 0.50);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Olives', 0.50);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Ham', 0.50);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Pineapple', 0.50);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Mushrooms', 0.50);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Mozzarella', 0.50);

INSERT INTO Pizza.Toppings
    ([Name], Price)
VALUES('Basil', 0.25);


-- Users
INSERT INTO Users.Users
    ([Name])
VALUES('Ian');

INSERT INTO Users.Users
    ([Name])
VALUES('Fred');


-- Stores
INSERT INTO Store.Store
    ([Name])
VALUES('Store1');

INSERT INTO Store.Store
    ([Name])
VALUES('Store2');
GO



SELECT *
FROM Pizza.Pizza;

SELECT *
FROM Orders.Orders;

SELECT *
FROM Pizza.PizzaTopping;



SELECT *
FROM Pizza.Crust;

SELECT *
FROM Pizza.[Size];

SELECT *
FROM Pizza.Toppings;

SELECT *
FROM Users.Users;

SELECT *
FROM Store.Store;