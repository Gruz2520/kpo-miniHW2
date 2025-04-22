using System;
using System.Collections.Generic;
using System.Linq;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;
using kpo_minihw2.Domain.ValueObjects;

namespace kpo_minihw2.Infrastructure.Repositories;

public class EnclosureRepository : IEnclosureRepository
{
    private readonly List<Enclosure> _enclosures = new();

    public Enclosure GetById(Guid id)
    {
        return _enclosures.FirstOrDefault(e => e.Id == id)
               ?? throw new KeyNotFoundException("Enclosure not found.");
    }

    public Enclosure FindEnclosureByAnimal(Animal animal)
    {
        return _enclosures.FirstOrDefault(e => e.Animals.Contains(animal));
    }

    public Animal FindAnimalById(Guid animalId)
    {
        // Проходим по всем вольерам и их животным, чтобы найти нужное животное
        foreach (var enclosure in _enclosures)
        {
            var animal = enclosure.Animals.FirstOrDefault(a => a.Id == animalId);
            if (animal != null)
                return animal;
        }
        throw new KeyNotFoundException("Animal not found.");
    }

    public void Add(Enclosure enclosure)
    {
        if (_enclosures.Any(e => e.Id == enclosure.Id))
            throw new InvalidOperationException("Enclosure with this ID already exists.");
        _enclosures.Add(enclosure);
    }

    public void Remove(Guid id)
    {
        var enclosure = _enclosures.FirstOrDefault(e => e.Id == id);
        if (enclosure == null)
            throw new KeyNotFoundException("Enclosure not found.");
        _enclosures.Remove(enclosure);
    }

    public List<Enclosure> GetAll()
    {
        return _enclosures;
    }

    public Enclosure FindAvailableEnclosureFor(Species species)
    {
        return _enclosures.FirstOrDefault(e => e.CurrentCapacity < e.MaxCapacity && e.Type.IsCompatibleWith(species))
               ?? throw new InvalidOperationException("No available enclosure for this species.");
    }
}