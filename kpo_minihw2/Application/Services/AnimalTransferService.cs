using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Events;

public class AnimalTransferService
{
    private readonly IEnclosureRepository _enclosureRepository;

    public AnimalTransferService(IEnclosureRepository enclosureRepository)
    {
        _enclosureRepository = enclosureRepository;
    }

    public void TransferAnimal(Guid animalId, Guid targetEnclosureId)
    {
        var animal = _enclosureRepository.FindAnimalById(animalId)
                      ?? throw new InvalidOperationException("Animal not found!");

        var targetEnclosure = _enclosureRepository.GetById(targetEnclosureId)
                              ?? throw new InvalidOperationException("Target enclosure not found!");

        var currentEnclosure = _enclosureRepository.FindEnclosureByAnimal(animal);

        currentEnclosure?.RemoveAnimal(animal);
        targetEnclosure.AddAnimal(animal);

        var animalMovedEvent = new AnimalMovedEvent(animal.Id, currentEnclosure?.Id, targetEnclosureId);
        DomainEvents.Raise(animalMovedEvent);
    }
}