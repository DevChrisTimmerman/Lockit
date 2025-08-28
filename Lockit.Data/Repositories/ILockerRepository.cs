using Lockit.Models;

namespace Lockit.Data.Repositories;

public interface ILockerRepository
{
	Task<List<Locker>> GetAllLockersAsync();
	Task<List<Locker>> GetLockersByLocationIdAsync(int locationId);
	Task<Locker?> GetLockerByIdAsync(int lockerId);
	Task<Locker?> GetLockerByUserIdAsync(int userId);
	Task<Locker> AddLockerAsync(Locker locker);
	Task<IEnumerable<Locker>> AddLockersBatchAsync(IEnumerable<Locker> lockers);
	Task<Locker> UpdateLockerAsync(Locker locker);
	Task<Locker> DeleteLockerAsync(int lockerId);
}
