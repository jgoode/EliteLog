using EliteModels;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Mappers {
    public class SystemPointerMapper {
        /// <summary>
        /// Returns a Expedition instance
        /// </summary>
        /// <param name="parse"></param>
        /// <returns></returns>
        public static SystemPointer Map(ParseObject parseObject) {
            SystemPointer sp = new SystemPointer();
            sp.ObjectId = parseObject.ObjectId;
            sp.CreatedAt = parseObject.CreatedAt.Value;
            sp.UpdatedAt = parseObject.UpdatedAt.Value;
            sp.CurrentObjectId = parseObject.Get<string>("currentObjectId");
            sp.LastObjectId = parseObject.Get<string>("lastObjectId");
            return sp;
        }
    }
}
