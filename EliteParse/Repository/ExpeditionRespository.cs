using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteParse.Models;
using Parse;
using EliteParse.Mappers;
using Repository;

namespace EliteParse.Repository {
    public class ExpeditionRespository : IExpeditionRepository {

       public async Task<Expedition> GetCurrent(string user) {
            var query = from item in ParseObject.GetQuery("Expedition")
                        where item.Get<int>("current") == 1 && item.Get<string>("username") == user
                        select item;

            ParseObject parseObject = await query.FirstAsync();
            var expedition = ExpeditionMapper.Map(parseObject);

            return expedition;
        }

        public async Task<IList<Expedition>> GetAll() {
            var query = ParseObject.GetQuery("Expedition");
            IEnumerable<ParseObject> items = await query.FindAsync();
            var expeditions = items.ToList().Select(p => ExpeditionMapper.Map(p));
            return expeditions.ToList();
        }

        public async Task<IList<Expedition>> GetByUser(string user) {
            var query = from item in ParseObject.GetQuery("Expedition")
                        where item.Get<string>("username") == user
                        select item;

            IEnumerable<ParseObject> items = await query.FindAsync();
            var expeditions = items.ToList().Select(p => ExpeditionMapper.Map(p));

            return expeditions.ToList();
        }

        public async Task<Expedition> GetOneById(string id) {
            throw new NotImplementedException();
        }

        public async Task<Expedition> GetByName(string name) {
            throw new NotImplementedException();
        }

        async Task<Expedition> IRepository<Expedition>.Save(Expedition entity) {
            return new Expedition();
        }
    }
}
