using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepo _repo;
        public EquipmentService(IEquipmentRepo repo) { _repo = repo; }

        public async Task<IEnumerable<EquipmentDTO>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(MapToDTO);

        public async Task<EquipmentDTO?> GetByIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : MapToDTO(e);
        }

        public async Task<bool> CreateAsync(EquipmentDTO dto, string? imagePath)
        {
            var equipment = new Equipment { Name = dto.Name, Description = dto.Description, Category = dto.Category, ImagePath = imagePath };
            await _repo.AddAsync(equipment);
            if (dto.Quantity.HasValue)
            {
                await _repo.AddInventoryAsync(new EquipmentInventory
                {
                    EquipmentId = equipment.Id, Quantity = dto.Quantity ?? 0,
                    StockStatus = dto.StockStatus ?? "Available",
                    PurchaseDate = dto.PurchaseDate, PurchasePrice = dto.PurchasePrice, Supplier = dto.Supplier
                });
            }
            return true;
        }

        public async Task<bool> UpdateAsync(EquipmentDTO dto, string? imagePath)
        {
            var e = await _repo.GetByIdAsync(dto.Id);
            if (e == null) return false;
            e.Name = dto.Name; e.Description = dto.Description; e.Category = dto.Category;
            if (imagePath != null) e.ImagePath = imagePath;
            await _repo.UpdateAsync(e);
            if (e.Inventory != null && dto.Quantity.HasValue)
            {
                e.Inventory.Quantity = dto.Quantity.Value;
                e.Inventory.StockStatus = dto.StockStatus ?? "Available";
                e.Inventory.PurchaseDate = dto.PurchaseDate;
                e.Inventory.PurchasePrice = dto.PurchasePrice;
                e.Inventory.Supplier = dto.Supplier;
                e.Inventory.LastUpdated = DateTime.Now;
                await _repo.UpdateInventoryAsync(e.Inventory);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id) { await _repo.DeleteAsync(id); return true; }

        public async Task<bool> UpdateInventoryAsync(int equipmentId, int quantity, string stockStatus, DateTime? purchaseDate, decimal? purchasePrice, string? supplier)
        {
            var e = await _repo.GetByIdAsync(equipmentId);
            if (e?.Inventory == null) return false;
            e.Inventory.Quantity = quantity; e.Inventory.StockStatus = stockStatus;
            e.Inventory.PurchaseDate = purchaseDate; e.Inventory.PurchasePrice = purchasePrice;
            e.Inventory.Supplier = supplier; e.Inventory.LastUpdated = DateTime.Now;
            await _repo.UpdateInventoryAsync(e.Inventory);
            return true;
        }

        private EquipmentDTO MapToDTO(Equipment e) => new()
        {
            Id = e.Id, Name = e.Name, Description = e.Description, Category = e.Category,
            ImagePath = e.ImagePath, IsActive = e.IsActive,
            Quantity = e.Inventory?.Quantity, StockStatus = e.Inventory?.StockStatus,
            PurchaseDate = e.Inventory?.PurchaseDate, PurchasePrice = e.Inventory?.PurchasePrice,
            Supplier = e.Inventory?.Supplier
        };
    }
}
