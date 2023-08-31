using PharmacyConsole.DAL.Entities;
using PharmacyConsole.DAL.Repositories.Interfaces;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Managers.Implementation
{
    public class BatchManager
    {
        private readonly IBatchRepository batchRepository;
        private readonly IBatchValidator batchValidator;

        public BatchManager(IBatchRepository batchRepository, IBatchValidator batchValidator)
        {
            this.batchRepository = batchRepository;
            this.batchValidator = batchValidator;
        }

        public async Task Insert(Batch entity)
        {
        
            if (!batchValidator.Validate(entity, out string error))
            {
                Console.WriteLine("Sorry, bad Validate\r\n" + error);
                return;
            }

            try
            {
                await batchRepository.Insert(entity);
                Console.WriteLine($"Batch was added {entity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await batchRepository.Delete(id);
                Console.WriteLine($"Batch with id {id} Deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }
    }
}
