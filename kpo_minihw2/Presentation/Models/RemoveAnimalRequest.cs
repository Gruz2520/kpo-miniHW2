using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpo_minihw2.Presentation.Models
{
    /// <summary>
    /// Модель запроса для удаления животного из вольера
    /// </summary>
    public class RemoveAnimalRequest
    {
        public Guid AnimalId { get; set; }
    }
}
