USE master
GO
if exists (select * from sysdatabases where name='Pharmacy')
	drop database Pharmacy
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE Pharmacy
  ON PRIMARY (NAME = N''Pharmacy'', FILENAME = N''' + @device_directory + N'pharmacy.mdf'')
  LOG ON (NAME = N''Pharmacy_log'',  FILENAME = N''' + @device_directory + N'pharmacy.ldf'')')
go

use "Pharmacy"
GO

if exists (select 1 from sys.objects where OBJECT_ID =OBJECT_ID('[dbo].[Batches]'))
	drop table [dbo].[Batches]
GO
if exists (select 1 from sys.objects where OBJECT_ID =OBJECT_ID('[dbo].[Warehouses]'))
	drop table [dbo].[Warehouses]
GO
if exists (select 1 from sys.objects where OBJECT_ID =OBJECT_ID('[dbo].[Pharmacies]'))
	drop table [dbo].[Pharmacies]
GO
if exists (select 1 from sys.objects where OBJECT_ID =OBJECT_ID('[dbo].[Products]'))
	drop table [dbo].[Products]
GO

CREATE TABLE [Products] (
	[ProductID] INT IDENTITY (1, 1) NOT NULL ,
	[ProductName] NVARCHAR (40) NOT NULL ,
	CONSTRAINT [PK_Products] PRIMARY KEY  CLUSTERED 
	(
		[ProductID]
	)
)
GO
CREATE TABLE [Pharmacies] (
	[PharmacyID] INT IDENTITY (1, 1) NOT NULL ,
	[PharmacyName] NVARCHAR (40) NOT NULL ,
	[PhoneNumber] NVARCHAR (40) NULL  ,
	CONSTRAINT [PK_Pharmacies] PRIMARY KEY  CLUSTERED 
	(
		[PharmacyID]
	)
)
GO

CREATE TABLE [dbo].[Warehouses] (
	[WarehouseID] INT IDENTITY (1, 1) NOT NULL ,
	[WarehouseName] NVARCHAR (40) NOT NULL ,
	[PharmacyID] INT NOT NULL  ,
	CONSTRAINT [PK_Warehouses] PRIMARY KEY  CLUSTERED 
	(
		[WarehouseID]
	),
	CONSTRAINT [FK_Warehouses_Pharmacies] FOREIGN KEY 
	(
		[PharmacyID]
	) REFERENCES [dbo].[Pharmacies] (
		[PharmacyID]
	),
)
GO
CREATE TABLE [dbo].[Batches] (
	[BatchID] INT IDENTITY (1, 1) NOT NULL ,
	[ProductID] INT NOT NULL  ,
	[WarehouseID] INT NOT NULL ,
	[Quantity] INT NOT NULL
	CONSTRAINT [PK_Batches] PRIMARY KEY  CLUSTERED 
	(
		[BatchID]
	),
	CONSTRAINT [FK_Batches_Products] FOREIGN KEY 
	(
		[ProductID]
	) REFERENCES [dbo].[Products] (
		[ProductID]
	),
	CONSTRAINT [FK_Batches_Warehouses] FOREIGN KEY 
	(
		[WarehouseID]
	) REFERENCES [dbo].[Warehouses] (
		[WarehouseID]
	),
)
GO
/*
DELETE FROM [dbo].[Batches]
DELETE FROM [dbo].[Warehouses]
DELETE FROM [dbo].[Pharmacies]
DELETE FROM [dbo].[Products]

set identity_insert [dbo].[Products] on
INSERT INTO [dbo].[Products]([ProductID], [ProductName]) VALUES(1, 'ProductName_1')
INSERT INTO [dbo].[Products]([ProductID], [ProductName]) VALUES(2, 'ProductName_2')
INSERT INTO [dbo].[Products]([ProductID], [ProductName]) VALUES(3, 'ProductName_3')
INSERT INTO [dbo].[Products]([ProductID], [ProductName]) VALUES(4, 'ProductName_4')
INSERT INTO [dbo].[Products]([ProductID], [ProductName]) VALUES(5, 'ProductName_5')
set identity_insert [dbo].[Products] off

set identity_insert [dbo].[Pharmacies] on
INSERT INTO [dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(1, 'PharmacyName_1', 'PhoneNumber_1')
INSERT INTO [dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(2, 'PharmacyName_2', 'PhoneNumber_2')
INSERT INTO [dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(3, 'PharmacyName_3', 'PhoneNumber_3')
INSERT INTO [dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(4, 'PharmacyName_4', 'PhoneNumber_4')
INSERT INTO [dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(5, 'PharmacyName_5', 'PhoneNumber_5')
set identity_insert [dbo].[Pharmacies] off

set identity_insert [dbo].[Warehouses] on
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(1, 'WarehouseName_1', 1)
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(2, 'WarehouseName_2', 1)
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(3, 'WarehouseName_3', 1)
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(4, 'WarehouseName_4', 2)
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(5, 'WarehouseName_5', 2)
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(6, 'WarehouseName_6', 3)
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(7, 'WarehouseName_7', 4)
INSERT INTO [dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(8, 'WarehouseName_8', 5)
set identity_insert [dbo].[Warehouses] off

set identity_insert [dbo].[Batches] on
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(1, 1, 1, 5)
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(2, 1, 2, 10)
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(3, 1, 3, 15)
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(4, 2, 1, 2)
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(5, 2, 2, 4)
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(6, 3, 3, 3)
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(7, 4, 4, 4)
INSERT INTO [dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(8, 5, 5, 5)
set identity_insert [dbo].[Batches] off
*/