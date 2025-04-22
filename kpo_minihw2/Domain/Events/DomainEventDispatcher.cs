using kpo_minihw2.Application.Interfaces;

namespace kpo_minihw2.Domain.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    public async Task Dispatch<T>(T domainEvent) where T : class
    {
        Console.WriteLine($"Event published: {domainEvent.GetType().Name}");
        await DomainEvents.Raise(domainEvent); 
    }
}