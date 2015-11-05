using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EliteModels {
    public class StarSystemObject : IEntity {
        public int Id { get; set; }
        [Required]
        public StarSystem StarSystem { get; set; }
        public string ObjectType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
