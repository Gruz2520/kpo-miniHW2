using System.Collections.Generic;
using System.Linq;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;

namespace kpo_minihw2.Application.Services;

public class ZooStatisticsService : IZooStatisticsService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public ZooStatisticsService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
    }

    public int GetTotalAnimalsCount()
    {
        return _animalRepository.GetAll().Count;
    }

    public int GetFreeEnclosuresCount()
    {
        return _enclosureRepository.GetAll().Count(e => e.CurrentCapacity < e.MaxCapacity);
    }

    public Dictionary<string, int> GetAnimalsBySpecies()
    {
        var animals = _animalRepository.GetAll();
        return animals
            .GroupBy(a => a.Species.Name)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public List<Enclosure> GetOvercrowdedEnclosures()
    {
        return _enclosureRepository.GetAll().Where(e => e.CurrentCapacity > e.MaxCapacity).ToList();
    }
}