
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteModels {
    /// <summary>
    /// Expedition Entity
    /// </summary>
    public class Expedition : IEntity{
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string StartSystem { get; set; }
        public string EndSystem { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Profit { get; set; }
        public double TotalDistance { get; set; }
        public bool Current { get; set; }
        public string User { get; set; }

        public List<StarSystem> StarSystems { get; set; }
    }
}
