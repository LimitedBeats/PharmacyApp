using PharmacyConsole.DAL.Entities;
using PharmacyConsole.Validators.Interfaces;

namespace PharmacyConsole.Validators.Implementation
{
    public class ProductValidator : IProductValidator
    {
        public bool Validate(Product entity, out string error)
        {
            error = "";
            var result = true;

            if (string.IsNullOrWhiteSpace(entity.ProductName) || entity.ProductName.Length < 3)
            {
                result = false;
                error = "ProductName is empty or Length < 3\r\n";
            }

            return result;
        }
    }
}
