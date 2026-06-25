using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface IMemberRepo
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member?> GetByUserIdAsync(string userId);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<Member>> SearchAsync(string query);
    }
}
