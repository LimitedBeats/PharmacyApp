using PharmacyConsole.DAL.Dto;

namespace PharmacyConsole.DAL.Repositories.Interfaces
{
    public interface ICommandRepository
    {
        Task<List<QuantityProductDto>> SelectQuantityProduct(int PharmacyID);
    }
}
