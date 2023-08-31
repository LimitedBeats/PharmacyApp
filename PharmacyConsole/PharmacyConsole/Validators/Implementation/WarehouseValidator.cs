using PharmacyConsole.DAL.Entities;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Validators.Implementation
{
    public class WarehouseValidator : IWarehouseValidator
    {
        public bool Validate(Warehouse entity, out string error)
        {
            var result = true;
            error = "";

            if (string.IsNullOrWhiteSpace(entity.WarehouseName) || entity.WarehouseName.Length < 3)
            {
                result = false;
                error = "WarehouseName is empty or Length < 3\r\n";
            }
            return result;
        }
    }
}
