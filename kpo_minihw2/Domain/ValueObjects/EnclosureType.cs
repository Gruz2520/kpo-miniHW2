namespace kpo_minihw2.Domain.ValueObjects;

public record EnclosureType(string Name)
{
    /// <summary>
    /// Проверяет, совместим ли тип вольера с данным видом животного
    /// </summary>
    public bool IsCompatibleWith(Species species)
    {
        if (Name == species.Name || species.DietType == DietType.Omnivore)
            return true;

        return false; 
    }
}