using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.Validators.Interfaces
{
    public interface IProductValidator
    {
        bool Validate(Product entity, out string error);
    }
}
