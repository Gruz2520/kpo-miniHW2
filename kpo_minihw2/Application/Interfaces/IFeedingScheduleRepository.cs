using System;
using System.Collections.Generic;
using kpo_minihw2.Domain.Entities;

namespace kpo_minihw2.Application.Interfaces;

public interface IFeedingScheduleRepository
{
    void Add(FeedingSchedule schedule);
    FeedingSchedule GetById(Guid id);
    List<FeedingSchedule> GetAll();
    void MarkAsCompleted(Guid id);
}