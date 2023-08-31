using PharmacyConsole.DAL.Entities;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Validators.Implementation
{
    public class PharmacyValidator : IPharmacyValidator
    {
        public bool Validate(Pharmacy entity, out string error)
        {
            error = "";
            var result = true;

            if (string.IsNullOrWhiteSpace(entity.PharmacyName) || entity.PharmacyName.Length < 5 || entity.PharmacyName.Length > 20)
            {
                result = false;
                error = "PharmacyName is empty or PharmacyName Length < 5 or PharmacyName > 20\r\n";
            }

            return result;
        }
    }
}
