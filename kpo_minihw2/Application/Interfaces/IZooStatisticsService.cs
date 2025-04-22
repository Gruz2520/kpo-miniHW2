using System.Collections.Generic;
using kpo_minihw2.Domain.Entities;

namespace kpo_minihw2.Application.Interfaces;

public interface IZooStatisticsService
{
    int GetTotalAnimalsCount();
    int GetFreeEnclosuresCount();
    Dictionary<string, int> GetAnimalsBySpecies();
    List<Enclosure> GetOvercrowdedEnclosures();
}