using Microsoft.AspNetCore.Mvc;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;
using kpo_minihw2.Domain.ValueObjects;
using kpo_minihw2.Presentation.Models;
using kpo_minihw2.Infrastructure.Repositories;

namespace kpo_minihw2.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    private readonly AnimalTransferService _animalTransferService;

    public AnimalsController(
        IAnimalRepository animalRepository,
        AnimalTransferService animalTransferService)
    {
        _animalRepository = animalRepository;
        _animalTransferService = animalTransferService;
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var animal = _animalRepository.GetById(id);
        return animal != null ? Ok(animal) : NotFound("Animal not found.");
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var animals = _animalRepository.GetAll();
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult Add([FromBody] AnimalRequest request)
    {
        var animal = new Animal(
            request.Name,
            new Species(request.Species, request.DietType), 
            request.BirthDate,
            request.Gender,
            new FoodType(request.FavoriteFood),
            request.IsHealthy 
        );

        _animalRepository.Add(animal);
        return CreatedAtAction(nameof(Get), new { id = animal.Id }, animal);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _animalRepository.Remove(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/treat")]
    public IActionResult TreatAnimal(Guid id)
    {
        var animal = _animalRepository.GetById(id);
        if (animal == null)
            return NotFound("Animal not found.");

        animal.Treat(); 
        return Ok(new { message = "Animal treated successfully.", isHealthy = animal.IsHealthy });
    }

    [HttpPost("{id}/make-sick")]
    public IActionResult MakeAnimalSick(Guid id)
    {
        var animal = _animalRepository.GetById(id);
        if (animal == null)
            return NotFound("Animal not found.");

        animal.MakeSick(); 
        return Ok(new { message = "Animal marked as sick.", isHealthy = animal.IsHealthy });
    }

    [HttpPost("{id}/feed")]
    public IActionResult FeedAnimal(Guid id, [FromBody] FeedAnimalRequest request)
    {
        var animal = _animalRepository.GetById(id);
        if (animal == null)
            return NotFound("Animal not found.");

        var foodType = new FoodType(request.FoodTypeName);

        try
        {
            animal.Feed(foodType); 
            return Ok(new { message = "Animal fed successfully.", isHealthy = animal.IsHealthy });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Переместить животное в другой вольер
    /// </summary>
    [HttpPost("{animalId}/move-to/{newEnclosureId}")]
    public IActionResult MoveAnimalToEnclosure(Guid animalId, Guid newEnclosureId)
    {
        try
        {
            _animalTransferService.TransferAnimal(animalId, newEnclosureId);

            return Ok(new { message = $"Animal with ID {animalId} moved to enclosure {newEnclosureId}." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}