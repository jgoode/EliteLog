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
    public class ExpeditionRespository : IExpeditionRepository {

       public async Task<Expedition> GetCurrent(string user) {
            var query = from item in ParseObject.GetQuery("Expedition")
                        where item.Get<int>("current") == 1 && item.Get<string>("username") == user
                        select item;

            ParseObject parseObject = await query.FirstAsync();
            var expedition = ExpeditionMapper.Map(parseObject);

            return expedition;
        }

        public async Task<IEnumerable<Expedition>> GetAll() {
            var items = await GetAllParseExpeditions();
            var expeditions = items.ToList().Select(p => ExpeditionMapper.Map(p));
            return expeditions;
        }

        public async Task<IEnumerable<ParseObject>> GetAllParseExpeditions() {
            var query = ParseObject.GetQuery("Expedition");
            return await query.FindAsync();
        }

        public async Task<IEnumerable<ParseObject>> GetAllParseCurrentExpeditions() {
            var query = ParseObject.GetQuery("Expedition")
                .WhereEqualTo("current", 1);
               
            return await query.FindAsync();
        }

        public async Task UnflagCurrentExpeditions() {
            IEnumerable<ParseObject> expeditions = await GetAllParseCurrentExpeditions();
            expeditions.ToList().ForEach(async p => {
                p["current"] = 0;
                await p.SaveAsync();
            });
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

        async Task<Expedition> IRepository<Expedition>.Insert(Expedition entity) {
            ParseObject expedition = new ParseObject("Expedition");
            expedition["name"] = entity.Name;
            expedition["description"] = entity.Description;
            expedition["beginDate"] = entity.StartDate;
            expedition["endDate"] = entity.EndDate;
            expedition["startSystem"] = entity.StartSystem;
            expedition["endSystem"] = "none";
            expedition["username"] = entity.User;
            expedition["current"] = entity.Current ? 1 : 0;
            expedition["profit"] = 0;
            expedition["totalDistance"] = 0;
            expedition["scanCountTotal"] = 0;
            expedition["systemVisitCount"] = 0;
            await expedition.SaveAsync(); 
            return ExpeditionMapper.Map(expedition);
        }

        public Task<Expedition> Udpate(Expedition entity) {
            throw new NotImplementedException();
        }
    }
}
