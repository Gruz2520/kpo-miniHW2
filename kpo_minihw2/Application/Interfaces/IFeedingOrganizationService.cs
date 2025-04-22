using kpo_minihw2.Domain.ValueObjects;

public interface IFeedingOrganizationService
{
    void AddFeedingSchedule(Guid animalId, DateTime feedingTime, FoodType foodType);
    void MarkFeedingAsCompleted(Guid feedingScheduleId);
}