using Lockit.Models;

namespace Lockit.Data.Repositories;

public interface ILockerRepository
{
	Task<List<Locker>> GetAllLockersAsync();
	Task<Locker?> GetLockerByIdAsync(int lockerId);
	Task<Locker?> GetLockerByUserIdAsync(int userId);
	Task<Locker> AddLockerAsync(Locker locker);
	Task<Locker> UpdateLockerAsync(Locker locker);
	Task<Locker> DeleteLockerAsync(int lockerId);
}
