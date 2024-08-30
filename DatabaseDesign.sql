-- Table to store information about customers
-- 
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    CreatedOn DATETIME NULL,
    CreatedBy INT NULL,
    UpdatedOn DATETIME NULL,
    UpdatedBy INT NULL
);

-- Table to store information about merchants
CREATE TABLE Merchants (
    MerchantId INT PRIMARY KEY IDENTITY(1,1),
    MerchantName NVARCHAR(100) NOT NULL,
    ContactEmail NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    Address NVARCHAR(255) NULL,
    CreatedOn DATETIME NULL,
    CreatedBy INT NULL,
    UpdatedOn DATETIME NULL,
    UpdatedBy INT NULL
);

-- Table to store transaction information
CREATE TABLE Transactions (
    TransactionId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(CustomerId),
    MerchantId INT NOT NULL FOREIGN KEY REFERENCES Merchants(MerchantId),
    Amount DECIMAL(18, 2) NOT NULL,
    TransactionDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(50) CHECK (Status IN ('Pending', 'Completed', 'Failed')) NOT NULL
);