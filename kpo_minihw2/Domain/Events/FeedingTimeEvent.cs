using System;

namespace kpo_minihw2.Domain.Events;

public class FeedingTimeEvent
{
    public Guid FeedingScheduleId { get; }
    public Guid AnimalId { get; }
    public DateTime FeedingTime { get; }
    public DateTime Timestamp { get; }

    public FeedingTimeEvent(Guid feedingScheduleId, Guid animalId, DateTime feedingTime)
    {
        FeedingScheduleId = feedingScheduleId;
        AnimalId = animalId;
        FeedingTime = feedingTime;
        Timestamp = DateTime.UtcNow;
    }
}