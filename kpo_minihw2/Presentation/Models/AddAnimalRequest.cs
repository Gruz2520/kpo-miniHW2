using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpo_minihw2.Presentation.Models
{
    /// <summary>
    /// Модель запроса для добавления животного в вольер
    /// </summary>
    public class AddAnimalRequest
    {
        public Guid AnimalId { get; set; }
    }
}
