using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitPanel.Entities.Concrete
{
    public class SportPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public int SportTypeId { get; set; }
        public SportType? SportType { get; set; }
    }
}
