using System;
using System.Collections.Generic;
using System.Linq;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;

namespace kpo_minihw2.Infrastructure.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly List<Animal> _animals = new();

    public Animal GetById(Guid id)
    {
        return _animals.FirstOrDefault(a => a.Id == id)
               ?? throw new KeyNotFoundException("Animal not found.");
    }

    public void Add(Animal animal)
    {
        if (_animals.Any(a => a.Id == animal.Id))
            throw new InvalidOperationException("Animal with this ID already exists.");
        _animals.Add(animal);
    }

    public void Remove(Guid id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
            throw new KeyNotFoundException("Animal not found.");
        _animals.Remove(animal);
    }

    public List<Animal> GetAll()
    {
        return _animals;
    }
}