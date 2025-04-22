using Microsoft.AspNetCore.Mvc;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;
using kpo_minihw2.Domain.ValueObjects;
using kpo_minihw2.Application.Services;
using kpo_minihw2.Presentation.Models;

namespace kpo_minihw2.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingScheduleController : ControllerBase
{
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly FeedingOrganizationService _feedingService;

    public FeedingScheduleController(
        IFeedingScheduleRepository feedingScheduleRepository,
        FeedingOrganizationService feedingService)
    {
        _feedingScheduleRepository = feedingScheduleRepository;
        _feedingService = feedingService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var schedules = _feedingScheduleRepository.GetAll();
            return Ok(schedules);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching feeding schedules: {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult Add([FromBody] AddFeedingScheduleRequest request)
    {
        try
        {
            var foodType = new FoodType(request.FoodTypeName);
            _feedingService.AddFeedingSchedule(request.AnimalId, request.FeedingTime, foodType);

            return CreatedAtAction(nameof(GetAll), new { });
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid input: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding a feeding schedule: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSchedule(Guid id, [FromBody] UpdateFeedingScheduleRequest request)
    {
        try
        {
            var schedule = _feedingScheduleRepository.GetById(id);
            if (schedule == null)
                return NotFound("Feeding schedule not found.");

            var newFoodType = new FoodType(request.FoodTypeName);
            schedule.UpdateSchedule(request.NewFeedingTime, newFoodType);

            return Ok(new { message = "Feeding schedule updated successfully." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid input: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the feeding schedule: {ex.Message}");
        }
    }

    [HttpPatch("{id}/complete")]
    public IActionResult CompleteFeeding(Guid id)
    {
        try
        {
            _feedingService.MarkFeedingAsCompleted(id);
            return Ok(new { message = "Feeding completed successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while completing the feeding: {ex.Message}");
        }
    }
}



