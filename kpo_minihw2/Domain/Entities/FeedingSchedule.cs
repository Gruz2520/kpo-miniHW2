using System;
using kpo_minihw2.Domain.ValueObjects;

namespace kpo_minihw2.Domain.Entities;

public class FeedingSchedule
{
    public Guid Id { get; private set; }
    public Animal Animal { get; private set; }
    public DateTime FeedingTime { get; private set; }
    public FoodType Food { get; private set; } 
    public bool IsCompleted { get; private set; }

    public FeedingSchedule(Animal animal, DateTime feedingTime, FoodType food)
    {
        Id = Guid.NewGuid();
        Animal = animal;
        FeedingTime = feedingTime;
        Food = food;
        IsCompleted = false;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    public void UpdateSchedule(DateTime newFeedingTime, FoodType newFood)
    {
        FeedingTime = newFeedingTime;
        Food = newFood;
    }
}