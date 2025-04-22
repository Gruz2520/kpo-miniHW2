using System.Threading.Tasks;

namespace kpo_minihw2.Application.Interfaces;

public interface IDomainEventDispatcher
{
    Task Dispatch<T>(T domainEvent) where T : class;
}