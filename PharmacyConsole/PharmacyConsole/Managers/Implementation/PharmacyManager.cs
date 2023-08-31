using PharmacyConsole.DAL.DataAccess;
using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;
using PharmacyConsole.Managers.Interfaces;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Managers.Implementation
{
    public class PharmacyManager
    {
        private readonly IWarehouseManager warehouseManager;
        private readonly IPharmacyRepository pharmacyRepository;
        private readonly IPharmacyValidator pharmacyValidator;

        public PharmacyManager(IPharmacyRepository pharmacyRepository, IWarehouseManager warehouseManager, IPharmacyValidator pharmacyValidator)
        {
            this.pharmacyRepository = pharmacyRepository;
            this.warehouseManager = warehouseManager;
            this.pharmacyValidator = pharmacyValidator;
        }

        public async Task Insert(Pharmacy entity)
        {
            try
            {
                if (!pharmacyValidator.Validate(entity, out string sError))
                {
                    Console.WriteLine("Sorry, bad Validate\r\n" + sError);
                    return;
                }

                await pharmacyRepository.Insert(entity);
                Console.WriteLine($"Pharmacy was added {entity}");
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
                var entity = await pharmacyRepository.Get(id);

                if (entity == null)
                {
                    Console.WriteLine($"Pharmacy not found");
                    return;
                }

                context = await pharmacyRepository.StartTransactionAsync();

                foreach (var warehouse in entity.WarehouseList)
                {
                    await warehouseManager.Delete(warehouse.WarehouseId);
                }

                await pharmacyRepository.Delete(entity.PharmacyId, context);

                context.Commit();

                Console.WriteLine($"Batch was delete {entity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                context?.Rollback();
            }
        }
    }
}
