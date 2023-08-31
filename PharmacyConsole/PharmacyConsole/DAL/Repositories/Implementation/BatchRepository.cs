using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.DataAccess.Extensions;
using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;

namespace PharmacyConsole.DAL.Repositories.Implementation
{
    public class BatchRepository : BaseRepository, IBatchRepository
    {
        public BatchRepository(IDbSettings settings) : base(settings)
        {
        }

        public async Task Delete(int batchId, TransactionContext context = null)
        {
            await ExecuteAsync("DELETE FROM [dbo].[Batches] WHERE [BatchID] = @BatchID",
                context,
                CancellationToken.None,
                command => command.AddParam("@BatchID", batchId));
        }

        public async Task Insert(Batch batch)
        {
            await ExecuteAsync("INSERT INTO [dbo].[Batches](WarehouseID, ProductID, Quantity) VALUES(@WarehouseID, @ProductID, @Quantity)",
                command => command
                .AddParam("@WarehouseID", batch.WarehouseId)
                .AddParam("@ProductID", batch.ProductId)
                .AddParam("@Quantity", batch.Quantity));
        }

        public async Task<Batch> Get(int id)
        {
            var sql = "SELECT * FROM [dbo].[Batches] b WHERE b.[BatchID] = @BatchID ";
            return await ExecuteAsync(sql, reader => new Batch
            {
                BatchId = reader.GetInt("BatchId"),
                ProductId = reader.GetInt("ProductId"),
                Quantity = reader.GetInt("Quantity"),
                WarehouseId = reader.GetInt("WarehouseId"),
            },
            command => command.AddParam("@BatchID", id));          
        }
    }
}
