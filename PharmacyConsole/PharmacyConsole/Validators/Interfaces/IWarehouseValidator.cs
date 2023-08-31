using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.Validators.Interfaces
{
    public interface IWarehouseValidator
    {
        bool Validate(Warehouse entity, out string error);
    }
}
