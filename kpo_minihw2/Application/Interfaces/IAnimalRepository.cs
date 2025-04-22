using System.Collections.Generic;
using kpo_minihw2.Domain.Entities;

namespace kpo_minihw2.Application.Interfaces;

public interface IAnimalRepository
{
    Animal GetById(Guid id);
    void Add(Animal animal);
    void Remove(Guid id);
    List<Animal> GetAll();
}