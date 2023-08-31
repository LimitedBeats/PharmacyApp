using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.DataAccess.Extensions;
using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;

namespace PharmacyConsole.DAL.Repositories.Implementation
{
    public class PharmacyRepository : BaseRepository, IPharmacyRepository
    {
        public PharmacyRepository(IDbSettings settings) : base(settings)
        {
        }

        public async Task Delete(int pharmacyId, TransactionContext context = null)
        {
            await ExecuteAsync("DELETE FROM [dbo].[Pharmacies] WHERE [PharmacyID] = @PharmacyID",
                context,
                CancellationToken.None,
                command => command
                .AddParam("@PharmacyID", pharmacyId));
        }

        public async Task Insert(Pharmacy pharmacy)
        {
            await ExecuteAsync("INSERT INTO [dbo].[Pharmacies](PharmacyName, PhoneNumber) VALUES(@PharmacyName, @PhoneNumber)",
                command => command
                .AddParam("@PharmacyName", pharmacy.PharmacyName)
                .AddParam("@PhoneNumber", pharmacy.PhoneNumber));
        }

        public async Task<Pharmacy> Get(int pharmacyId)
        {
            var result = new Pharmacy();
            await ExecuteReaderAsync(
               "SELECT * FROM [dbo].[Pharmacies] p WHERE p.[PharmacyID] = @PharmacyID " +
               "SELECT * FROM [dbo].[Warehouses] w WHERE w.[PharmacyID] = @PharmacyID ",
                async r =>
                {
                    while (await r.ReadAsync())
                    {
                        result.PharmacyId = r.GetInt("PharmacyID");
                        result.PharmacyName = r.GetString("PharmacyName");
                    }

                    if (await r.NextResultAsync())
                    {
                        var warehouseList = new List<Warehouse>();

                        while (await r.ReadAsync())
                            warehouseList.Add(new Warehouse()
                            {
                                WarehouseId = r.GetInt("WarehouseID"),
                                WarehouseName = r.GetString("WarehouseName"),
                            });

                        result.WarehouseList = warehouseList;

                    }
                },
                command => command
                .AddParam("@PharmacyID", pharmacyId)
                );
            return result;
        }
    }
}
