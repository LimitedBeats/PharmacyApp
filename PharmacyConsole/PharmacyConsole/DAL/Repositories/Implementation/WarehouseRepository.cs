using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.DataAccess.Extensions;
using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;

namespace PharmacyConsole.DAL.Repositories.Implementation
{
    public class WarehouseRepository : BaseRepository, IWarehouseRepository
    {
        public WarehouseRepository(IDbSettings settings) : base(settings)
        {
        }

        public async Task Delete(int warehouseId, TransactionContext context = null)
        {
            await ExecuteAsync("DELETE FROM [dbo].[Warehouses] WHERE [WarehouseID] = @WarehouseID",
                context,
                CancellationToken.None,
                command => command
                .AddParam("@WarehouseID", warehouseId));
        }

        public async Task Insert(Warehouse warehouse)
        {
            await ExecuteAsync("INSERT INTO [dbo].[Warehouses](WarehouseName, PharmacyID) VALUES(@WarehouseName, @PharmacyID)",
                command => command
                .AddParam("@WarehouseName", warehouse.WarehouseName)
                .AddParam("@PharmacyID", warehouse.PharmacyId));
        }

        public async Task<Warehouse> Get(int warehouseId)
        {
            var result = new Warehouse();
            await ExecuteReaderAsync(
               "SELECT * FROM [dbo].[Warehouses] w WHERE w.[WarehouseID] = @WarehouseID " +
               "SELECT * FROM [dbo].[Batches] b WHERE b.[WarehouseID] = @WarehouseID",
                async r =>
                {
                    while (await r.ReadAsync())
                    {
                        result.WarehouseId = r.GetInt("WarehouseID");
                        result.WarehouseName = r.GetString("WarehouseName");
                        result.PharmacyId = r.GetInt("PharmacyID");
                    }

                    if (await r.NextResultAsync())
                    {
                        var batchList = new List<Batch>();

                        while (await r.ReadAsync())
                            batchList.Add(new Batch()
                            {
                                BatchId = r.GetInt("BatchID"),
                                Quantity = r.GetInt("Quantity"),
                            });

                        result.BatchList = batchList;

                    }
                },
                command => command
                .AddParam("@WarehouseID", warehouseId)
                );
            return result;
        }
    }
}
