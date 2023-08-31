using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.DAL.Repositories.Interfaces
{
    public interface IPharmacyRepository
    {
        Task<TransactionContext> StartTransactionAsync();
        Task Insert(Pharmacy pharmacy);
        Task Delete(int pharmacyId, TransactionContext context = null);
        Task<Pharmacy> Get(int pharmacyId);
    }
}
