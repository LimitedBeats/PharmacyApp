using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Managers.Implementation
{
    public class ProductManager
    {
        private readonly IProductRepository productRepository;
        private readonly IBatchRepository batchRepository;
        private readonly IProductValidator productValidator;

        public ProductManager(IProductRepository productRepository, IBatchRepository batchRepository, IProductValidator productValidator)
        {
            this.productRepository = productRepository;
            this.batchRepository = batchRepository;
            this.productValidator = productValidator;
        }

        public async Task Insert(Product entity)
        {
            try
            {
                if (!productValidator.Validate(entity, out string sError))
                {
                    Console.WriteLine("Sorry, bad Validate\r\n" + sError);
                    return;
                }

                await productRepository.Insert(entity);
                Console.WriteLine($"Product was added {entity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            TransactionContext context = null;
            try
            {
                var entity = await productRepository.Get(id);

                if (entity == null)
                {
                    Console.WriteLine($"Product not found");
                    return;
                }

                context = await productRepository.StartTransactionAsync();

                foreach (var batch in entity.BatchList)
                {
                    await batchRepository.Delete(batch.BatchId, context);
                }

                await productRepository.Delete(entity.ProductId, context);

                context.Commit();

                Console.WriteLine($"Product was Delete {entity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                context?.Rollback();
            }
        }
    }
}
