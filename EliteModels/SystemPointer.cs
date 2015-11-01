using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteModels {
    public class SystemPointer: IEntity {
        public string ObjectId { get; set; }
        public string Expedition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CurrentObjectId { get; set; }
        public string LastObjectId { get; set; }
    }
}
