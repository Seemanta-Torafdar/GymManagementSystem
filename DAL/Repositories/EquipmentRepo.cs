using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EquipmentRepo : IEquipmentRepo
    {
        private readonly GymDbContext _context;
        public EquipmentRepo(GymDbContext context) { _context = context; }

        public async Task<IEnumerable<Equipment>> GetAllAsync() =>
            await _context.Equipments.Include(e => e.Inventory).Where(e => e.IsActive).ToListAsync();
        public async Task<Equipment?> GetByIdAsync(int id) =>
            await _context.Equipments.Include(e => e.Inventory).FirstOrDefaultAsync(e => e.Id == id);
        public async Task AddAsync(Equipment equipment) { await _context.Equipments.AddAsync(equipment); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Equipment equipment) { _context.Equipments.Update(equipment); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var e = await _context.Equipments.FindAsync(id);
            if (e != null) { e.IsActive = false; await _context.SaveChangesAsync(); }
        }
        public async Task<IEnumerable<EquipmentInventory>> GetAllInventoryAsync() =>
            await _context.EquipmentInventories.Include(ei => ei.Equipment).ToListAsync();
        public async Task AddInventoryAsync(EquipmentInventory inventory) { await _context.EquipmentInventories.AddAsync(inventory); await _context.SaveChangesAsync(); }
        public async Task UpdateInventoryAsync(EquipmentInventory inventory) { _context.EquipmentInventories.Update(inventory); await _context.SaveChangesAsync(); }
    }
}
