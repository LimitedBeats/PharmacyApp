using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.DataAccess.Extensions;
using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;

namespace PharmacyConsole.DAL.Repositories.Implementation
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDbSettings settings) : base(settings)
        {
        }

        public async Task Delete(int productId, TransactionContext context = null)
        {
            await ExecuteAsync("DELETE FROM [dbo].[Products] WHERE [ProductID] = @ProductID",
                context,
                CancellationToken.None,
                command => command
                .AddParam("@ProductID", productId));
        }

        public async Task Insert(Product product)
        {
            await ExecuteAsync("INSERT INTO [dbo].[Products](ProductName) VALUES(@productName)", command => command.AddParam("@productName", product.ProductName));
        }

        public async Task<Product> Get(int productId)
        {
            var result = new Product();
            await ExecuteReaderAsync(
               "SELECT * FROM [dbo].[Products] p WHERE p.[ProductID] = @ProductID " +
               "SELECT * FROM [dbo].[Batches] b WHERE b.[ProductID] = @ProductID",
                async r =>
                {
                    while (await r.ReadAsync())
                    {
                        result.ProductId = r.GetInt("ProductID");
                        result.ProductName = r.GetString("ProductName");
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
                .AddParam("@ProductID", productId)
                );
            return result;
        }
    }
}
