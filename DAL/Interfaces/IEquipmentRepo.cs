using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface IEquipmentRepo
    {
        Task<IEnumerable<Equipment>> GetAllAsync();
        Task<Equipment?> GetByIdAsync(int id);
        Task AddAsync(Equipment equipment);
        Task UpdateAsync(Equipment equipment);
        Task DeleteAsync(int id);
        Task<IEnumerable<EquipmentInventory>> GetAllInventoryAsync();
        Task AddInventoryAsync(EquipmentInventory inventory);
        Task UpdateInventoryAsync(EquipmentInventory inventory);
    }
}
