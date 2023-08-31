using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.DAL.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<TransactionContext> StartTransactionAsync();
        Task Insert(Product product);
        Task Delete(int productId, TransactionContext context = null);
        Task<Product> Get(int productId);
    }
}
