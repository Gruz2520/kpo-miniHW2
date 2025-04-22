using kpo_minihw2.Domain.Events;
using kpo_minihw2.Domain.ValueObjects;
using kpo_minihw2.Application.Interfaces; 
using System;

namespace kpo_minihw2.Domain.Entities;

public class Animal
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Species Species { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; } 
    public FoodType FavoriteFood { get; private set; }
    public bool IsHealthy { get; private set; } 

    public Animal(string name, Species species, DateTime birthDate, Gender gender, FoodType favoriteFood, bool isHealthy = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Species = species;
        BirthDate = birthDate;
        Gender = gender;
        FavoriteFood = favoriteFood;
        IsHealthy = isHealthy;
    }

    /// <summary>
    /// Метод для кормления животного
    /// </summary>
    public void Feed(FoodType food)
    {
        if (!IsHealthy)
            throw new InvalidOperationException("The animal is sick and cannot be fed until treated.");

        if (food != FavoriteFood)
            throw new InvalidOperationException("This animal doesn't like this food!");

        Console.WriteLine($"{Name} has been fed with {food.Name}.");
    }

    /// <summary>
    /// Метод для лечения животного
    /// </summary>
    public void Treat()
    {
        IsHealthy = true;
        Console.WriteLine($"{Name} has been treated and is now healthy.");
    }

    /// <summary>
    /// Метод для установки статуса "больной"
    /// </summary>
    public void MakeSick()
    {
        IsHealthy = false;
        Console.WriteLine($"{Name} is now sick.");
    }

    /// <summary>
    /// Метод для перемещения животного в новый вольер
    /// </summary>
    public void MoveToEnclosure(Enclosure currentEnclosure, Enclosure newEnclosure, IDomainEventDispatcher eventDispatcher)
    {
        if (currentEnclosure == null || !currentEnclosure.Animals.Contains(this))
            throw new InvalidOperationException("Animal is not in the current enclosure.");

        if (newEnclosure.Type.IsCompatibleWith(Species) && newEnclosure.CurrentCapacity < newEnclosure.MaxCapacity)
        {
            currentEnclosure.RemoveAnimal(this);
            newEnclosure.AddAnimal(this);

            // Публикуем событие о перемещении животного
            var animalMovedEvent = new AnimalMovedEvent(Id, currentEnclosure.Id, newEnclosure.Id);
            eventDispatcher.Dispatch(animalMovedEvent);

            Console.WriteLine($"{Name} has been moved to a new enclosure.");
        }
        else
        {
            throw new InvalidOperationException("Cannot move the animal to the new enclosure due to incompatibility or capacity issues.");
        }
    }
}