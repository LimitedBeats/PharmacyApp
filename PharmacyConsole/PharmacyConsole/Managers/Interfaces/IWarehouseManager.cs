using PharmacyConsole.DAL.Entities;

namespace PharmacyConsole.Managers.Interfaces
{
    public interface IWarehouseManager
    {
        Task Insert(Warehouse entity);
        Task Delete(int id);
    }
}
