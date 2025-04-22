using System;
using System.Collections.Generic;
using System.Linq;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Entities;

namespace kpo_minihw2.Infrastructure.Repositories;

public class FeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly List<FeedingSchedule> _schedules = new();

    public FeedingSchedule GetById(Guid id)
    {
        return _schedules.FirstOrDefault(s => s.Id == id)
               ?? throw new KeyNotFoundException("Feeding schedule not found.");
    }

    public void Add(FeedingSchedule schedule)
    {
        if (_schedules.Any(s => s.Id == schedule.Id))
            throw new InvalidOperationException("Feeding schedule with this ID already exists.");
        _schedules.Add(schedule);
    }

    public void MarkAsCompleted(Guid id)
    {
        var schedule = _schedules.FirstOrDefault(s => s.Id == id);
        if (schedule == null)
            throw new KeyNotFoundException("Feeding schedule not found.");
        schedule.MarkAsCompleted();
    }

    public List<FeedingSchedule> GetUpcomingFeedings()
    {
        return _schedules.Where(s => !s.IsCompleted && s.FeedingTime > DateTime.UtcNow).ToList();
    }

    public List<FeedingSchedule> GetAll()
    {
        return _schedules.ToList();
    }
}