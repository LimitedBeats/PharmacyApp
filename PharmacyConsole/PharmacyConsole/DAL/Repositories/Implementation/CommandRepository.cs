using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.DataAccess.Extensions;
using PharmacyConsole.DAL.Dto;
using PharmacyConsole.DAL.Repositories.Interfaces;

namespace PharmacyConsole.DAL.Repositories.Implementation
{
    public class CommandRepository : BaseRepository, ICommandRepository
    {
        public CommandRepository(IDbSettings settings) : base(settings)
        {
        }

        public async Task<List<QuantityProductDto>> SelectQuantityProduct(int PharmacyID)
        {
            var sql = "SELECT b.ProductID, p.ProductName, SUM(Quantity) AS Quantity, w.PharmacyID, ph.PharmacyName " +
                    "FROM[dbo].[Batches] b " +
                    "JOIN[dbo].[Warehouses] w ON w.WarehouseID = b.WarehouseID " +
                    "JOIN[dbo].[Products] p ON p.ProductID = b.ProductID " +
                    "JOIN[dbo].[Pharmacies] ph ON ph.PharmacyID = w.PharmacyID " +
                    "WHERE w.PharmacyID = @PharmacyID " +
                    "GROUP BY b.ProductID, p.ProductName, w.PharmacyID, ph.PharmacyName";
            return await ExecuteListAsync(sql, reader => new QuantityProductDto
            {
                ProductID = reader.GetInt("ProductID"),
                ProductName = reader.GetString("ProductName"),
                Quantity = reader.GetInt("Quantity"),
                PharmacyID = reader.GetInt("PharmacyID"),
                PharmacyName = reader.GetString("PharmacyName"),
            },
            command => command.AddParam("@PharmacyID", PharmacyID));
        }
    }
}
