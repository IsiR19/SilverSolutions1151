Build started...
Build succeeded.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'Price' on entity type 'Catalog'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'Quantity' on entity type 'ManufacturingStage'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'Quantity' on entity type 'Packaging'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'ClosedBalance' on entity type 'ProductionReport'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'InProgress' on entity type 'ProductionReport'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'OpeningBalance' on entity type 'ProductionReport'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'Packing' on entity type 'ProductionReport'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'ReadyStockk' on entity type 'ProductionReport'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'Sold' on entity type 'ProductionReport'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'Price' on entity type 'InvoiceDetails'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
      No store type was specified for the decimal property 'VAT' on entity type 'InvoiceDetails'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 6.0.10 initialized 'ApplicationDbContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer:6.0.10' with options: None
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customers] (
    [CustomerId] uniqueidentifier NOT NULL,
    [CustomerName] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [ZipCode] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [ContactPerson] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
);
GO

CREATE TABLE [Packaging] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Quantity] decimal(18,2) NULL,
    CONSTRAINT [PK_Packaging] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProductionReport] (
    [Id] uniqueidentifier NOT NULL,
    [OpeningBalance] decimal(18,2) NOT NULL,
    [ClosedBalance] decimal(18,2) NOT NULL,
    [InProgress] decimal(18,2) NOT NULL,
    [ReadyStockk] decimal(18,2) NOT NULL,
    [Sold] decimal(18,2) NOT NULL,
    [Packing] decimal(18,2) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProductionReport] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProductTypeIngredients] (
    [Id] uniqueidentifier NOT NULL,
    [ProductTypeId] nvarchar(max) NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductTypeIngredients] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SalesOrder] (
    [SalesOrderId] int NOT NULL IDENTITY,
    [SalesOrderName] nvarchar(max) NOT NULL,
    [BranchId] int NOT NULL,
    [CustomerId] int NOT NULL,
    [OrderDate] datetimeoffset NOT NULL,
    [DeliveryDate] datetimeoffset NOT NULL,
    [CurrencyId] int NOT NULL,
    [CustomerRefNumber] nvarchar(max) NOT NULL,
    [SalesTypeId] int NOT NULL,
    [Remarks] nvarchar(max) NOT NULL,
    [Amount] float NOT NULL,
    [SubTotal] float NOT NULL,
    [Discount] float NOT NULL,
    [Tax] float NOT NULL,
    [Freight] float NOT NULL,
    [Total] float NOT NULL,
    CONSTRAINT [PK_SalesOrder] PRIMARY KEY ([SalesOrderId])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Invoice] (
    [InvoiceID] uniqueidentifier NOT NULL,
    [InvoiceNumber] int NOT NULL,
    [CustomerID] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NOT NULL,
    [ProposalDetails] nvarchar(max) NOT NULL,
    [TimeStamp] datetime2 NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [Paid] bit NOT NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY ([InvoiceID]),
    CONSTRAINT [FK_Invoice_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductType] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [PackagingId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductType_Packaging_PackagingId] FOREIGN KEY ([PackagingId]) REFERENCES [Packaging] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SalesOrderLine] (
    [SalesOrderLineId] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Quantity] float NOT NULL,
    [Price] float NOT NULL,
    [Amount] float NOT NULL,
    [DiscountPercentage] float NOT NULL,
    [DiscountAmount] float NOT NULL,
    [SubTotal] float NOT NULL,
    [TaxPercentage] float NOT NULL,
    [TaxAmount] float NOT NULL,
    [Total] float NOT NULL,
    CONSTRAINT [PK_SalesOrderLine] PRIMARY KEY ([SalesOrderLineId]),
    CONSTRAINT [FK_SalesOrderLine_SalesOrder_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrder] ([SalesOrderId]) ON DELETE CASCADE
);
GO

CREATE TABLE [InvoiceDetails] (
    [Id] uniqueidentifier NOT NULL,
    [InvoiceID] uniqueidentifier NOT NULL,
    [Article] nvarchar(max) NOT NULL,
    [Qty] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [VAT] decimal(18,2) NOT NULL,
    [TimeStamp] datetime2 NOT NULL,
    CONSTRAINT [PK_InvoiceDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InvoiceDetails_Invoice_InvoiceID] FOREIGN KEY ([InvoiceID]) REFERENCES [Invoice] ([InvoiceID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Catalog] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ProductTypeId] uniqueidentifier NOT NULL,
    [Price] decimal(18,2) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Catalog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Catalog_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Ingredients] (
    [Id] uniqueidentifier NOT NULL,
    [ProductTypeId] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Ratio] int NULL,
    CONSTRAINT [PK_Ingredients] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ingredients_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Packing] (
    [Id] uniqueidentifier NOT NULL,
    [CatalogId] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_Packing] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Packing_Catalog_CatalogId] FOREIGN KEY ([CatalogId]) REFERENCES [Catalog] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RawMaterials] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CatalogId] uniqueidentifier NULL,
    [ProductTypeId] uniqueidentifier NULL,
    [CreatedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RawMaterials] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RawMaterials_Catalog_CatalogId] FOREIGN KEY ([CatalogId]) REFERENCES [Catalog] ([Id]),
    CONSTRAINT [FK_RawMaterials_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id])
);
GO

CREATE TABLE [ManufacturingStage] (
    [Id] uniqueidentifier NOT NULL,
    [ProductionStage] int NOT NULL,
    [ProductTypes] uniqueidentifier NOT NULL,
    [ProductTypeId] uniqueidentifier NULL,
    [Ingredients] uniqueidentifier NOT NULL,
    [IngredientId] uniqueidentifier NULL,
    [Quantity] decimal(18,2) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_ManufacturingStage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ManufacturingStage_Ingredients_IngredientId] FOREIGN KEY ([IngredientId]) REFERENCES [Ingredients] ([Id]),
    CONSTRAINT [FK_ManufacturingStage_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id])
);
GO

CREATE TABLE [Inventory] (
    [Id] uniqueidentifier NOT NULL,
    [Catalog Item] uniqueidentifier NOT NULL,
    [RawMaterialId] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [Balance] int NOT NULL,
    [IsSellable] bit NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [ModifiedBy] nvarchar(max) NOT NULL,
    [CreatedModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Inventory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Inventory_RawMaterials_RawMaterialId] FOREIGN KEY ([RawMaterialId]) REFERENCES [RawMaterials] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_Catalog_ProductTypeId] ON [Catalog] ([ProductTypeId]);
GO

CREATE INDEX [IX_Ingredients_ProductTypeId] ON [Ingredients] ([ProductTypeId]);
GO

CREATE INDEX [IX_Inventory_RawMaterialId] ON [Inventory] ([RawMaterialId]);
GO

CREATE INDEX [IX_Invoice_CustomerID] ON [Invoice] ([CustomerID]);
GO

CREATE INDEX [IX_InvoiceDetails_InvoiceID] ON [InvoiceDetails] ([InvoiceID]);
GO

CREATE INDEX [IX_ManufacturingStage_IngredientId] ON [ManufacturingStage] ([IngredientId]);
GO

CREATE INDEX [IX_ManufacturingStage_ProductTypeId] ON [ManufacturingStage] ([ProductTypeId]);
GO

CREATE INDEX [IX_Packing_CatalogId] ON [Packing] ([CatalogId]);
GO

CREATE INDEX [IX_ProductType_PackagingId] ON [ProductType] ([PackagingId]);
GO

CREATE INDEX [IX_RawMaterials_CatalogId] ON [RawMaterials] ([CatalogId]);
GO

CREATE INDEX [IX_RawMaterials_ProductTypeId] ON [RawMaterials] ([ProductTypeId]);
GO

CREATE INDEX [IX_SalesOrderLine_SalesOrderId] ON [SalesOrderLine] ([SalesOrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221126145517_initial', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ManufacturingStage] DROP CONSTRAINT [FK_ManufacturingStage_Ingredients_IngredientId];
GO

ALTER TABLE [ManufacturingStage] DROP CONSTRAINT [FK_ManufacturingStage_ProductType_ProductTypeId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ManufacturingStage]') AND [c].[name] = N'Ingredients');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ManufacturingStage] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ManufacturingStage] DROP COLUMN [Ingredients];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ManufacturingStage]') AND [c].[name] = N'ProductTypes');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ManufacturingStage] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ManufacturingStage] DROP COLUMN [ProductTypes];
GO

DROP INDEX [IX_ManufacturingStage_ProductTypeId] ON [ManufacturingStage];
DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ManufacturingStage]') AND [c].[name] = N'ProductTypeId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ManufacturingStage] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ManufacturingStage] ALTER COLUMN [ProductTypeId] uniqueidentifier NOT NULL;
ALTER TABLE [ManufacturingStage] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [ProductTypeId];
CREATE INDEX [IX_ManufacturingStage_ProductTypeId] ON [ManufacturingStage] ([ProductTypeId]);
GO

DROP INDEX [IX_ManufacturingStage_IngredientId] ON [ManufacturingStage];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ManufacturingStage]') AND [c].[name] = N'IngredientId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ManufacturingStage] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [ManufacturingStage] ALTER COLUMN [IngredientId] uniqueidentifier NOT NULL;
ALTER TABLE [ManufacturingStage] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [IngredientId];
CREATE INDEX [IX_ManufacturingStage_IngredientId] ON [ManufacturingStage] ([IngredientId]);
GO

ALTER TABLE [ManufacturingStage] ADD CONSTRAINT [FK_ManufacturingStage_Ingredients_IngredientId] FOREIGN KEY ([IngredientId]) REFERENCES [Ingredients] ([Id]);
GO

ALTER TABLE [ManufacturingStage] ADD CONSTRAINT [FK_ManufacturingStage_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221126162642_new', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221201110532_InvoiceUpdate', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221201110839_InvoiceUpdate1', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Invoice] ADD [paidstatus] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221201111056_InvoiceUpdate2', N'6.0.10');
GO

COMMIT;
GO


