namespace kpo_minihw2.Domain.Events;

public class AnimalMovedEvent
{
    public Guid AnimalId { get; }
    public Guid? SourceEnclosureId { get; }
    public Guid TargetEnclosureId { get; }
    public DateTime Timestamp { get; }

    public AnimalMovedEvent(Guid animalId, Guid? sourceEnclosureId, Guid targetEnclosureId)
    {
        AnimalId = animalId;
        SourceEnclosureId = sourceEnclosureId;
        TargetEnclosureId = targetEnclosureId;
        Timestamp = DateTime.UtcNow;
    }
}