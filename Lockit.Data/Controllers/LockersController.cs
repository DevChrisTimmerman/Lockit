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

	[HttpGet("location/{locationId}")]
	public async Task<IActionResult> GetLockersByLocationId(int locationId)
	{
		var lockers = await _lockerRepository.GetLockersByLocationIdAsync(locationId);
		if (lockers == null || !lockers.Any())
		{
			return NotFound();
		}
		return Ok(lockers);
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

	[HttpPost("batch")]
	public async Task<IActionResult> AddLockersBatch(List<Locker> lockers)
	{
		if (lockers == null || !lockers.Any())
		{
			return BadRequest("Locker list is null or empty.");
		}
		var createdLockers = await _lockerRepository.AddLockersBatchAsync(lockers);
		return CreatedAtAction(nameof(GetAllLockers), new { }, createdLockers);
	}

	[HttpPut("{lockerId}")]
	public async Task<IActionResult> UpdateLocker(int lockerId, Locker locker)
	{
		if (locker == null || locker.ID != lockerId)
		{
			return BadRequest("Locker data is invalid.");
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
