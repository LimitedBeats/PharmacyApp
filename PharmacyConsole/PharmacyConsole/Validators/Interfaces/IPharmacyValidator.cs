using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.Validators.Interfaces
{
    public interface IPharmacyValidator
    {
        bool Validate(Pharmacy entity, out string error);
    }
}
