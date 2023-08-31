using PharmacyConsole.DAL.DataAccess;

namespace PharmacyConsole.DAL.Repositories.Implementation
{
    public class CommonRepository : BaseRepository
    {
        public CommonRepository(IDbSettings settings) : base(settings)
        {
        }
         
        public async Task RepairData()
        {
            var sql = "DELETE FROM [dbo].[Batches] " +
                    "DELETE FROM[dbo].[Warehouses] " +
                    " DELETE FROM[dbo].[Pharmacies] " +
                    " DELETE FROM[dbo].[Products] " +
                    "set identity_insert[dbo].[Products] on " +
                    "INSERT INTO[dbo].[Products]([ProductID], [ProductName]) VALUES(1, 'ProductName_1') " +
                    "INSERT INTO[dbo].[Products]([ProductID], [ProductName]) VALUES(2, 'ProductName_2') " +
                    "INSERT INTO[dbo].[Products]([ProductID], [ProductName]) VALUES(3, 'ProductName_3') " +
                    "INSERT INTO[dbo].[Products]([ProductID], [ProductName]) VALUES(4, 'ProductName_4') " +
                    "INSERT INTO[dbo].[Products]([ProductID], [ProductName]) VALUES(5, 'ProductName_5') " +
                    "set identity_insert[dbo].[Products] off " +
                    "set identity_insert[dbo].[Pharmacies] on " +
                    "INSERT INTO[dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(1, 'PharmacyName_1', 'PhoneNumber_1') " +
                    "INSERT INTO[dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(2, 'PharmacyName_2', 'PhoneNumber_2') " +
                    "INSERT INTO[dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(3, 'PharmacyName_3', 'PhoneNumber_3') " +
                    "INSERT INTO[dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(4, 'PharmacyName_4', 'PhoneNumber_4') " +
                    "INSERT INTO[dbo].[Pharmacies]([PharmacyID], [PharmacyName], [PhoneNumber]) VALUES(5, 'PharmacyName_5', 'PhoneNumber_5') " +
                    "set identity_insert[dbo].[Pharmacies] off " +
                    "set identity_insert[dbo].[Warehouses] on " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(1, 'WarehouseName_1', 1) " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(2, 'WarehouseName_2', 1) " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(3, 'WarehouseName_3', 1) " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(4, 'WarehouseName_4', 2) " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(5, 'WarehouseName_5', 2) " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(6, 'WarehouseName_6', 3) " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(7, 'WarehouseName_7', 4) " +
                    "INSERT INTO[dbo].[Warehouses]([WarehouseID], [WarehouseName], [PharmacyID]) VALUES(8, 'WarehouseName_8', 5) " +
                    "set identity_insert[dbo].[Warehouses] off " +
                    "set identity_insert[dbo].[Batches] on " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(1, 1, 1, 5)    " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(2, 1, 2, 10)   " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(3, 1, 3, 15)   " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(4, 2, 1, 2)    " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(5, 2, 2, 4)    " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(6, 3, 3, 3)    " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(7, 4, 4, 4)    " +
                    "INSERT INTO[dbo].[Batches]([BatchID], [ProductID], [WarehouseID], [Quantity]) VALUES(8, 5, 5, 5)    " +
                    "set identity_insert[dbo].[Batches] off";
            await ExecuteAsync(sql);
        }
    }
}
