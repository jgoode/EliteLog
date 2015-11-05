using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteModels;
using Parse;
using EliteParse.Mappers;
using Repository;

namespace EliteParse.Repository {
    public class SystemPointerRepository : ISystemPointerRepository {
        private Expedition _expedition;

        public Expedition Expedition { get { return _expedition; } set { _expedition = value; } }

        /// <summary>
        /// Empty argument constructor
        /// </summary>
        public SystemPointerRepository() {
        }
        
        public SystemPointerRepository(Expedition expedition) {
            _expedition = expedition;
        }

        public async Task<IEnumerable<SystemPointer>> GetAll() {
            var query = from item in ParseObject.GetQuery("SystemPointer")
                        where  item.Get<string>("expedition") == _expedition.ObjectId
                        select item;

            var items = await query.FindAsync();
            IEnumerable<SystemPointer> pointers = items.ToList().Select(p => SystemPointerMapper.Map(p));
            return pointers;
        }

        public async Task<ParseObject> GetCurrent() {
            var query = from item in ParseObject.GetQuery("SystemPointer")
                        where item.Get<string>("expedition") == _expedition.ObjectId
                        select item;
            ParseObject po = await query.FirstAsync();
            return po;
        }

        public async Task<ParseObject> Save(ParseObject po) {
            await po.SaveAsync();
            return po;
        }

        public async Task<SystemPointer> GetByName(string name) {
            throw new NotImplementedException();
        }

        public async Task<SystemPointer> GetOneById(string id) {
            throw new NotImplementedException();
        }

        public async Task<SystemPointer> Save(SystemPointer entity) {
            if (null == entity) throw new ArgumentNullException("SystemPointer");
            ParseObject systemPointer = new ParseObject("SystemPointer");
            
            systemPointer["expedition"] = _expedition.ObjectId;
            systemPointer["currentObjectId"] = entity.CurrentObjectId;
            systemPointer["lastObjectId"] = entity.LastObjectId;
            await systemPointer.SaveAsync();
            entity.ObjectId = systemPointer.ObjectId;
            entity.CreatedAt = systemPointer.CreatedAt.Value;
            entity.UpdatedAt = systemPointer.UpdatedAt.Value;
            return entity;
        }

        public Task<SystemPointer> Udpate(SystemPointer entity) {
            throw new NotImplementedException();
        }

        public Task<SystemPointer> Insert(SystemPointer entity) {
            throw new NotImplementedException();
        }
    }
}
