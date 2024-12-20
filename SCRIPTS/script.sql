
CREATE TABLE Users (
    UserId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Roles (
    RolId SERIAL PRIMARY KEY,
    RolName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE UsersInRoles (
    UserId INT REFERENCES Users(UserId) ON DELETE CASCADE,
    RolId INT REFERENCES Roles(RolId) ON DELETE CASCADE,
    PRIMARY KEY (UserId, RolId)
);

CREATE TABLE Products (
    ProductId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL UNIQUE,
    Inventory INT NOT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Transactions (
    TransactionId SERIAL PRIMARY KEY,
    ProductId INT REFERENCES Products(ProductId) ON DELETE CASCADE,
    UserId INT REFERENCES Users(UserId) ON DELETE CASCADE,
    Quantity INT NOT NULL,
    IsDeleted BOOLEAN DEFAULT FALSE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


INSERT INTO Roles (RolName) VALUES 
('Admin'),
('Supervisor'),
('Cashier'),
('Viewer');

INSERT INTO Users (Name, Email) VALUES 
('Jhonnier Castro', 'jhonnier@example.com'),
('Admin User', 'admin@example.com');

INSERT INTO UsersInRoles (UserId, RolId) VALUES 
(1, 1), -- Jhonnier como Admin
(2, 2), -- Admin User como Supervisor
(2, 3); -- Admin User como Cashier


INSERT INTO Products (Name, Inventory) VALUES 
('Producto A', 100),
('Producto B', 50),
('Producto C', 200);


select * from users;
select * from Roles;
select * from UsersInRoles;
select * from Products;
select * from Transactions;

select count(*) from Roles where Rolname in (Admin) 
