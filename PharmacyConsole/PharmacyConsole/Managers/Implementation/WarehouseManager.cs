using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;
using PharmacyConsole.Managers.Interfaces;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Managers.Implementation
{
    public class WarehouseManager : IWarehouseManager
    {
        private readonly IBatchRepository batchRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IWarehouseValidator warehouseValidator;

        public WarehouseManager(IBatchRepository batchRepository, IWarehouseRepository warehouseRepository, IWarehouseValidator warehouseValidator)
        {
            this.batchRepository = batchRepository;
            this.warehouseRepository = warehouseRepository;
            this.warehouseValidator = warehouseValidator;
        }

        public async Task Insert(Warehouse entity)
        {
            try
            {
                if (!warehouseValidator.Validate(entity, out string sError))
                {
                    Console.WriteLine("Sorry, bad Validate\r\n" + sError);
                    return;
                }
                await warehouseRepository.Insert(entity);
                Console.WriteLine($"Warehouse was added {entity}");
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
                var entity = await warehouseRepository.Get(id);

                if (entity == null)
                {
                    Console.WriteLine($"Product not found");
                    return;
                }
                context = await warehouseRepository.StartTransactionAsync();

                int test = 0;
                foreach (var batch in entity.BatchList)
                {
                    await batchRepository.Delete(batch.BatchId, context);
                    test++;
                }
                await warehouseRepository.Delete(entity.WarehouseId, context);

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
