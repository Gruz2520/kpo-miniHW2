using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;
using kpo_minihw2.Domain.ValueObjects;
using kpo_minihw2.Presentation.Models;

namespace kpo_minihw2.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly IAnimalRepository _animalRepository;

    public EnclosuresController(IEnclosureRepository enclosureRepository, IAnimalRepository animalRepository)
    {
        _enclosureRepository = enclosureRepository;
        _animalRepository = animalRepository;
    }

    /// <summary>
    /// Получить вольер по ID
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        try
        {
            var enclosure = _enclosureRepository.GetById(id);
            return Ok(enclosure);
        }
        catch (Exception ex)
        {
            return NotFound($"Enclosure with ID {id} not found: {ex.Message}");
        }
    }

    /// <summary>
    /// Получить все вольеры
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var enclosures = _enclosureRepository.GetAll();
        return Ok(enclosures);
    }

    /// <summary>
    /// Добавить новый вольер
    /// </summary>
    [HttpPost]
    public IActionResult Add([FromBody] EnclosureRequest request)
    {
        try
        {
            var enclosure = new Enclosure(
                new EnclosureType(request.Type),
                request.Size,
                request.MaxCapacity
            );

            _enclosureRepository.Add(enclosure);
            return CreatedAtAction(nameof(Get), new { id = enclosure.Id }, enclosure);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add enclosure: {ex.Message}");
        }
    }

    /// <summary>
    /// Удалить вольер по ID
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _enclosureRepository.Remove(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound($"Failed to delete enclosure with ID {id}: {ex.Message}");
        }
    }

    /// <summary>
    /// Добавить животное в вольер
    /// </summary>
    [HttpPost("{id}/add-animal")]
    public IActionResult AddAnimalToEnclosure(Guid id, [FromBody] AddAnimalRequest request)
    {
        try
        {
            var enclosure = _enclosureRepository.GetById(id);
            if (enclosure == null)
                return NotFound($"Enclosure with ID {id} not found.");

            var animal = _animalRepository.GetById(request.AnimalId);
            if (animal == null)
                return NotFound($"Animal with ID {request.AnimalId} not found.");

            if (!enclosure.Type.IsCompatibleWith(animal.Species))
                return BadRequest($"Animal {animal.Name} cannot be placed in this enclosure due to type incompatibility.");

            if (enclosure.CurrentCapacity >= enclosure.MaxCapacity)
                return BadRequest($"Enclosure {enclosure.Id} is full and cannot accommodate more animals.");

            enclosure.AddAnimal(animal);
            return Ok(new { message = $"Animal {animal.Name} added to enclosure {enclosure.Id}." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add animal to enclosure: {ex.Message}");
        }
    }

    /// <summary>
    /// Удалить животное из вольера
    /// </summary>
    [HttpDelete("{id}/remove-animal")]
    public IActionResult RemoveAnimalFromEnclosure(Guid id, [FromBody] RemoveAnimalRequest request)
    {
        try
        {
            var enclosure = _enclosureRepository.GetById(id);
            if (enclosure == null)
                return NotFound($"Enclosure with ID {id} not found.");

            var animal = _animalRepository.GetById(request.AnimalId);
            if (animal == null)
                return NotFound($"Animal with ID {request.AnimalId} not found.");

            if (!enclosure.Animals.Contains(animal))
                return BadRequest($"Animal {animal.Name} is not in enclosure {enclosure.Id}.");

            enclosure.RemoveAnimal(animal);
            return Ok(new { message = $"Animal {animal.Name} removed from enclosure {enclosure.Id}." });
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to remove animal from enclosure: {ex.Message}");
        }
    }

    /// <summary>
    /// Провести уборку вольера
    /// </summary>
    [HttpPost("{id}/clean")]
    public IActionResult CleanEnclosure(Guid id)
    {
        try
        {
            var enclosure = _enclosureRepository.GetById(id);
            enclosure.Clean();
            return Ok(new { message = $"Enclosure {enclosure.Id} has been cleaned." });
        }
        catch (Exception ex)
        {
            return NotFound($"Failed to clean enclosure with ID {id}: {ex.Message}");
        }
    }
}


