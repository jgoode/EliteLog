using EliteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteService {
    public class StarSystemToGridRowService {
        public static IEnumerable<SystemGridRow> ConvertToGridRows(IEnumerable<StarSystem> systems, int returnNumber) {
            if (systems == null) throw new ArgumentNullException("systems");
            var gridRows = new List<SystemGridRow>();

            if (systems.Count() == 0) return gridRows;

            return (from p in systems
                        orderby p.CreatedAt descending
                        select new SystemGridRow {
                            Date = p.CreatedAt,
                            Distance = p.DistToNext,
                            Name = p.Name,
                            ObjectId = p.ObjectId
                        }).Take(returnNumber).ToList();    
        }
    }
}
