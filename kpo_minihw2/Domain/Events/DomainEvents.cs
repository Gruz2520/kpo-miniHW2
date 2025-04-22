using System;
using System.Collections.Generic;

namespace kpo_minihw2.Domain.Events;

public static class DomainEvents
{
    private static readonly List<Func<object, Task>> _handlers = new();

    public static void RegisterHandler<T>(Func<T, Task> handler) where T : class
    {
        _handlers.Add(async (domainEvent) =>
        {
            if (domainEvent is T eventOfType)
                await handler(eventOfType);
        });
    }

    public static async Task Raise(object domainEvent)
    {
        foreach (var handler in _handlers)
        {
            await handler(domainEvent);
        }
    }
}