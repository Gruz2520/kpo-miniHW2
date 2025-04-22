using kpo_minihw2.Domain.ValueObjects;

namespace kpo_minihw2.Presentation.Models;

public class AnimalRequest
{
    public string Name { get; set; }
    public string Species { get; set; } 
    public DietType DietType { get; set; } 
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string FavoriteFood { get; set; }
    public bool IsHealthy { get; set; }
}