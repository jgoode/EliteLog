using EliteParse.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Mappers {
    /// <summary>
    /// Maps Parse System object to StarSystem object
    /// </summary>
    public class ExpeditionMapper {
        /// <summary>
        /// Returns a Expedition instance
        /// </summary>
        /// <param name="parse"></param>
        /// <returns></returns>
        public static Expedition Map(ParseObject parseObject) {
            Expedition exp = new Expedition();
            exp.ObjectId = parseObject.ObjectId;
            exp.CreatedAt = parseObject.CreatedAt.Value;
            exp.UpdatedAt = parseObject.UpdatedAt.Value;
            exp.Name = parseObject.Get<string>("name");
            exp.Description = parseObject.Get<string>("description");
            exp.Current = parseObject.Get<int>("current") == 1;
            exp.EndSystem = parseObject.Get<string>("endSystem");
            exp.StartSystem = parseObject.Get<string>("startSystem");
            exp.StartDate = parseObject.Get<DateTime?>("beginDate");
            exp.EndDate = parseObject.Get<DateTime?>("endDate");
            exp.Profit = parseObject.Get<double>("profit");
            exp.TotalDistance = parseObject.Get<double>("totalDistance");
            return exp;
        }
    }
}
