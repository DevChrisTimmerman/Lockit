using Lockit.Data.Repositories;
using Lockit.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lockit.Data.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LockersController : Controller
{
	private readonly ILockerRepository _lockerRepository;

	public LockersController(ILockerRepository lockerRepository)
	{
		_lockerRepository = lockerRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllLockers()
	{
		return Ok(await _lockerRepository.GetAllLockersAsync());
	}

	[HttpGet("{lockerId}")]
	public async Task<IActionResult> GetLockerById(int lockerId)
	{
		var locker = await _lockerRepository.GetLockerByIdAsync(lockerId);
		if (locker == null)
		{
			return NotFound();
		}
		return Ok(locker);
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetLockerByUserId(int userId)
	{
		var locker = await _lockerRepository.GetLockerByUserIdAsync(userId);
		if (locker == null)
		{
			return NotFound();
		}
		return Ok(locker);
	}

	[HttpPost]
	public async Task<IActionResult> AddLocker(Locker locker)
	{
		if (locker == null)
		{
			return BadRequest("Locker data is null.");
		}
		var createdLocker = await _lockerRepository.AddLockerAsync(locker);
		return CreatedAtAction(nameof(GetLockerById), new { lockerId = createdLocker.ID }, createdLocker);
	}

	[HttpPut("{lockerId}")]
	public async Task<IActionResult> UpdateLocker(int lockerId, Locker locker)
	{
		if (locker == null || locker.ID != lockerId)
		{
			return BadRequest("Locker data is invalid.");
		}
		var existingLocker = await _lockerRepository.GetLockerByIdAsync(lockerId);
		if (existingLocker == null)
		{
			return NotFound();
		}
		var updatedLocker = await _lockerRepository.UpdateLockerAsync(locker);
		return Ok(updatedLocker);
	}

	[HttpDelete("{lockerId}")]
	public async Task<ActionResult<Locker>> DeleteLocker(int lockerId)
	{
		var lockerToDelete = await _lockerRepository.GetLockerByIdAsync(lockerId);
		if (lockerToDelete == null) return NotFound($"Locker with id = {lockerId} was not found!");
		return await _lockerRepository.DeleteLockerAsync(lockerId);
	}
}
