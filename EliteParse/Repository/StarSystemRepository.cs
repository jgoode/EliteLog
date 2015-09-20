using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteParse.Models;
using Parse;
using EliteParse.Mappers;

namespace EliteParse.Repository {

    /// <summary>
    /// Handles the persistance and querying to the EliteLog Parse
    /// NoSQL database
    /// </summary>
    public class StarSystemRepository : IStarSystemRepository {
        private Expedition _expedition;

        /// <summary>
        /// Constructor for StarSystemRepository Instance. Requires 
        /// Expedition instance
        /// </summary>
        /// <param name="expedition"></param>
        public StarSystemRepository(Expedition expedition) {
            _expedition = expedition;
        }

        /// <summary>
        /// Retrieves all StarSystems
        /// </summary>
        /// <returns>List of StarSystem instances</returns>
        public async Task<IList<StarSystem>> GetAll() {
            var n = 1000;
            var s = 0;
            var query = ParseObject.GetQuery("StarSystem");
            query.Limit(n).Skip(s).OrderBy("createdAt");
            IEnumerable<ParseObject> items = await query.FindAsync();
            var starSystems = items.ToList().Select(p => StarSystemMapper.Map(p));
            return starSystems.ToList();
        }

        /// <summary>
        /// Retrieve StarSystem by ObjectId. 
        /// </summary>
        /// <param name="id">Parse ObjectId</param>
        /// <returns>Single StarSystem instance</returns>
        public async Task<StarSystem> GetOneById(string id) {
            var query = from item in ParseObject.GetQuery("StarSystem")
                        where item.ObjectId == id &&
                        item.Get<string>("expedition") == _expedition.ObjectId
                        select item;

            ParseObject parseObject = await query.FirstAsync();
            var starSystem = StarSystemMapper.Map(parseObject);

            return starSystem;
        }

       /// <summary>
        /// Retrieve StarSystem instance by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Single StarSystem instance</returns>
       public async Task<StarSystem> GetByName(string name) {
            var query = from item in ParseObject.GetQuery("StarSystem")
                        where item.Get<string>("name") == name &&
                        item.Get<string>("expedition") == _expedition.ObjectId
                        select item;

            ParseObject parseObject = await query.FirstAsync();
            var starSystem = StarSystemMapper.Map(parseObject);

            return starSystem;
        }

        /// <summary>
        /// Persist StarSystem entity to Parse database
        /// </summary>
        /// <param name="entity">Entity to save</param>
        public void Save(StarSystem entity) {
            throw new NotImplementedException();
        }
    }
}
