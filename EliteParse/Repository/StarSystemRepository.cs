using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteParse.Models;
using Parse;
using EliteParse.Mappers;

namespace EliteParse.Repository {
    public class StarSystemRepository : IStarSystemRepository {
        public async Task<IList<StarSystem>> GetAll() {
            var n = 1000;
            var s = 0;
            var query = ParseObject.GetQuery("StarSystem");
            query.Limit(n).Skip(s).OrderBy("createdAt");
            IEnumerable<ParseObject> items = await query.FindAsync();
            var starSystems = items.ToList().Select(p => StarSystemMapper.Map(p));
            return starSystems.ToList();
        }

        public IList<StarSystem> GetByValue(StarSystem entity) {
            throw new NotImplementedException();
        }

        public StarSystem GetOneById(string id) {
            throw new NotImplementedException();
        }

        public void Save(StarSystem entity) {
            throw new NotImplementedException();
        }
    }
}
