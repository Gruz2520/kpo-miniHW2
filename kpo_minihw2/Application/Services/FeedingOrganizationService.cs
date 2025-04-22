using System;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;
using kpo_minihw2.Domain.ValueObjects;
using kpo_minihw2.Domain.Events;

namespace kpo_minihw2.Application.Services;

public class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public FeedingOrganizationService(
        IFeedingScheduleRepository feedingScheduleRepository,
        IAnimalRepository animalRepository,
        IDomainEventDispatcher eventDispatcher)
    {
        _feedingScheduleRepository = feedingScheduleRepository;
        _animalRepository = animalRepository;
        _eventDispatcher = eventDispatcher;
    }

    public void AddFeedingSchedule(Guid animalId, DateTime feedingTime, FoodType foodType)
    {
        var animal = _animalRepository.GetById(animalId)
                     ?? throw new InvalidOperationException("Animal not found.");

        var schedule = new FeedingSchedule(animal, feedingTime, foodType);

        _feedingScheduleRepository.Add(schedule);
    }

    public void MarkFeedingAsCompleted(Guid feedingScheduleId)
    {
        var schedule = _feedingScheduleRepository.GetById(feedingScheduleId)
                       ?? throw new InvalidOperationException("Feeding schedule not found.");

        var animal = schedule.Animal;

        try
        {
            animal.Feed(schedule.Food); 
            _feedingScheduleRepository.MarkAsCompleted(feedingScheduleId);

            // Публикуем событие о завершении кормления
            var feedingTimeEvent = new FeedingTimeEvent(
                schedule.Id,
                schedule.Animal.Id,
                schedule.FeedingTime
            );
            _eventDispatcher.Dispatch(feedingTimeEvent);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to feed the animal: {ex.Message}");
        }
    }
}