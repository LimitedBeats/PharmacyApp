using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.DAL.Repositories.Interfaces
{
    public interface IBatchRepository
    {
        Task Insert(Batch batch);
        Task Delete(int batchId, TransactionContext context = null);
    }
}
