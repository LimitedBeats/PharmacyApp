using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.Validators.Interfaces
{
    public interface IBatchValidator
    {
        bool Validate(Batch entity, out string error);
    }
}
