using System;
using System.Collections.Generic;
using kpo_minihw2.Domain.ValueObjects;

namespace kpo_minihw2.Domain.Entities;

public class Enclosure
{
    public Guid Id { get; private set; }
    public EnclosureType Type { get; private set; } 
    public int Size { get; private set; }
    public int CurrentCapacity { get; private set; }
    public int MaxCapacity { get; private set; }
    public List<Animal> Animals { get; private set; } = new();

    public Enclosure(EnclosureType type, int size, int maxCapacity)
    {
        Id = Guid.NewGuid();
        Type = type;
        Size = size;
        MaxCapacity = maxCapacity;
        CurrentCapacity = 0; // Инициализируем текущую вместимость
    }

    /// <summary>
    /// Добавляет животное в вольер
    /// </summary>
    public void AddAnimal(Animal animal)
    {
        if (Animals.Contains(animal))
            throw new InvalidOperationException($"Animal {animal.Name} is already in this enclosure.");

        if (CurrentCapacity >= MaxCapacity)
            throw new InvalidOperationException("Enclosure is full!");

        if (!Type.IsCompatibleWith(animal.Species))
            throw new InvalidOperationException("This animal cannot be placed in this enclosure!");

        Animals.Add(animal);
        CurrentCapacity++;
        Console.WriteLine($"{animal.Name} has been added to the enclosure.");
    }

    /// <summary>
    /// Удаляет животное из вольера
    /// </summary>
    public void RemoveAnimal(Animal animal)
    {
        if (!Animals.Contains(animal))
            throw new InvalidOperationException("This animal is not in the enclosure.");

        Animals.Remove(animal);
        CurrentCapacity--;
        Console.WriteLine($"{animal.Name} has been removed from the enclosure.");
    }

    /// <summary>
    /// Проводит уборку вольера
    /// </summary>
    public void Clean()
    {
        if (CurrentCapacity == 0)
        {
            Console.WriteLine("The enclosure is empty. No cleaning needed.");
            return;
        }

        Console.WriteLine($"Cleaning the enclosure of type {Type.Name}...");
        foreach (var animal in Animals)
        {
            Console.WriteLine($"Checking conditions for {animal.Name}...");
        }
        Console.WriteLine("Enclosure cleaned successfully.");
    }

    /// <summary>
    /// Возвращает информацию о вольере
    /// </summary>
    public override string ToString()
    {
        return $"Enclosure ID: {Id}, Type: {Type.Name}, Size: {Size}, " +
               $"Current Capacity: {CurrentCapacity}/{MaxCapacity}";
    }
}