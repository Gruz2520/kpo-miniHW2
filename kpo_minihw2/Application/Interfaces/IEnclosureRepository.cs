using System;
using kpo_minihw2.Domain.Entities;
using kpo_minihw2.Domain.ValueObjects;

namespace kpo_minihw2.Application.Interfaces;

public interface IEnclosureRepository
{
    Enclosure GetById(Guid id);
    Enclosure FindEnclosureByAnimal(Animal animal);
    Animal FindAnimalById(Guid animalId); 
    void Add(Enclosure enclosure);
    void Remove(Guid id);
    List<Enclosure> GetAll();
    Enclosure FindAvailableEnclosureFor(Species species);
}