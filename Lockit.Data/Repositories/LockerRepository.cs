using Lockit.Data.Models;
using Lockit.Models;
using Microsoft.EntityFrameworkCore;

namespace Lockit.Data.Repositories;

public class LockerRepository : ILockerRepository
{
	private readonly LockitDbContext _context;

	public LockerRepository(LockitDbContext context)
	{
		_context = context;
	}

	public async Task<List<Locker>> GetAllLockersAsync()
	{
		return await _context.Lockers.ToListAsync();
	}

	public async Task<Locker?> GetLockerByIdAsync(int lockerId)
	{
		return await _context.Lockers.FirstOrDefaultAsync(l => l.ID == lockerId);
	}

	public async Task<Locker?> GetLockerByUserIdAsync(int userId)
	{
		return await _context.Lockers.FirstOrDefaultAsync(l => l.StudentID == userId);
	}

	public async Task<Locker> AddLockerAsync(Locker locker)
	{
		var result = await _context.Lockers.AddAsync(locker);
		await _context.SaveChangesAsync();
		return result.Entity;
	}

	public async Task<Locker> UpdateLockerAsync(Locker locker)
	{
		_context.Lockers.Update(locker);
		await _context.SaveChangesAsync();
		return locker;
	}

	public async Task<Locker> DeleteLockerAsync(int lockerId)
	{
		var locker = await _context.Lockers.FindAsync(lockerId);
		if (locker != null)
		{
			_context.Lockers.Remove(locker);
			await _context.SaveChangesAsync();
			return locker;
		}

		return null;
	}
}
