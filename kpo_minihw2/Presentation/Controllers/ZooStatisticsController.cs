using Microsoft.AspNetCore.Mvc;
using kpo_minihw2.Application.Interfaces;
using System.Collections.Generic;

namespace kpo_minihw2.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZooStatisticsController : ControllerBase
{
    private readonly IZooStatisticsService _zooStatisticsService;

    public ZooStatisticsController(IZooStatisticsService zooStatisticsService)
    {
        _zooStatisticsService = zooStatisticsService;
    }

    /// <summary>
    /// Получить общее количество животных в зоопарке
    /// </summary>
    [HttpGet("total-animals")]
    public IActionResult GetTotalAnimalsCount()
    {
        try
        {
            var totalAnimals = _zooStatisticsService.GetTotalAnimalsCount();
            return Ok(new { totalAnimals });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Получить количество свободных вольеров
    /// </summary>
    [HttpGet("free-enclosures")]
    public IActionResult GetFreeEnclosuresCount()
    {
        try
        {
            var freeEnclosures = _zooStatisticsService.GetFreeEnclosuresCount();
            return Ok(new { freeEnclosures });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Получить распределение животных по видам
    /// </summary>
    [HttpGet("animals-by-species")]
    public IActionResult GetAnimalsBySpecies()
    {
        try
        {
            var animalsBySpecies = _zooStatisticsService.GetAnimalsBySpecies();
            return Ok(animalsBySpecies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Получить список переполненных вольеров
    /// </summary>
    [HttpGet("overcrowded-enclosures")]
    public IActionResult GetOvercrowdedEnclosures()
    {
        try
        {
            var overcrowdedEnclosures = _zooStatisticsService.GetOvercrowdedEnclosures();
            return Ok(overcrowdedEnclosures);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}