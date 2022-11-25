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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'00000000000000_CreateIdentitySchema', N'6.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE TABLE [ManufacturingStage] (
        [Id] uniqueidentifier NOT NULL,
        [ProductionStage] int NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ManufacturingStage] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE TABLE [Packaging] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Quantity] decimal(18,2) NULL,
        CONSTRAINT [PK_Packaging] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE TABLE [ProductType] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductType] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE TABLE [ProductTypeIngredients] (
        [Id] uniqueidentifier NOT NULL,
        [ProductTypeId] nvarchar(max) NOT NULL,
        [ProductName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductTypeIngredients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE TABLE [Ingredients] (
        [Id] uniqueidentifier NOT NULL,
        [ProductTypeId] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Quantity] decimal(18,2) NULL,
        [RawMaterialId] uniqueidentifier NOT NULL,
        [TotalQuantity] decimal(18,2) NULL,
        CONSTRAINT [PK_Ingredients] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Ingredients_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Ingredients_RawMaterials_RawMaterialId] FOREIGN KEY ([RawMaterialId]) REFERENCES [RawMaterials] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE INDEX [IX_Catalog_ProductTypeId] ON [Catalog] ([ProductTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE INDEX [IX_Ingredients_ProductTypeId] ON [Ingredients] ([ProductTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE INDEX [IX_Ingredients_RawMaterialId] ON [Ingredients] ([RawMaterialId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE INDEX [IX_Inventory_RawMaterialId] ON [Inventory] ([RawMaterialId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE INDEX [IX_RawMaterials_CatalogId] ON [RawMaterials] ([CatalogId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    CREATE INDEX [IX_RawMaterials_ProductTypeId] ON [RawMaterials] ([ProductTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121075055_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221121075055_initial', N'6.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121114010_CustomerDB')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121114010_CustomerDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221121114010_CustomerDB', N'6.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121121128_SalesOrder')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121121128_SalesOrder')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121121128_SalesOrder')
BEGIN
    CREATE INDEX [IX_SalesOrderLine_SalesOrderId] ON [SalesOrderLine] ([SalesOrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221121121128_SalesOrder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221121121128_SalesOrder', N'6.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221123224709_productType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221123224709_productType', N'6.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221123231626_PackagingId')
BEGIN
    ALTER TABLE [ProductType] ADD [PackagingId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221123231626_PackagingId')
BEGIN
    CREATE INDEX [IX_ProductType_PackagingId] ON [ProductType] ([PackagingId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221123231626_PackagingId')
BEGIN
    ALTER TABLE [ProductType] ADD CONSTRAINT [FK_ProductType_Packaging_PackagingId] FOREIGN KEY ([PackagingId]) REFERENCES [Packaging] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221123231626_PackagingId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221123231626_PackagingId', N'6.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221124001251_Packing')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221124001251_Packing', N'6.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221124001730_packingdata')
BEGIN
    CREATE TABLE [Packing] (
        [Id] uniqueidentifier NOT NULL,
        [CatalogId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        CONSTRAINT [PK_Packing] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Packing_Catalog_CatalogId] FOREIGN KEY ([CatalogId]) REFERENCES [Catalog] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221124001730_packingdata')
BEGIN
    CREATE INDEX [IX_Packing_CatalogId] ON [Packing] ([CatalogId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221124001730_packingdata')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221124001730_packingdata', N'6.0.10');
END;
GO

COMMIT;
GO

