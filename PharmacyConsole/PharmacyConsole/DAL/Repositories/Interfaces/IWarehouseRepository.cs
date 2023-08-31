using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.DAL.Repositories.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<TransactionContext> StartTransactionAsync();
        Task Insert(Warehouse warehouse);
        Task Delete(int warehouseId, TransactionContext context = null);
        Task<Warehouse> Get(int warehouseId);
    }
}
