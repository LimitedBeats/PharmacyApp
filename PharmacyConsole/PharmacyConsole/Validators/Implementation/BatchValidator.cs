using PharmacyConsole.DAL.Entities;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Validators.Implementation
{
    public class BatchValidator : IBatchValidator
    {
        public bool Validate(Batch entity, out string error)
        {
            error = "";
            var result = true;

            if (entity.Quantity < 0)
            {
                result = false;
                error = "Quantity < 0\r\n";
            }

            return result;
        }
    }
}
