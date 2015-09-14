using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteParse.Models;
using Parse;
using EliteParse.Mappers;

namespace EliteParse.Repository {
    public class ExpeditionRespository : IExpeditionRepository {

       public async Task<Expedition> GetCurrent() {
            var query = from item in ParseObject.GetQuery("Expedition")
                        where item.Get<int>("current") == 1
                        select item;

            ParseObject parseObject = await query.FirstAsync();
            var expedition = ExpeditionMapper.Map(parseObject);

            return expedition;
        }

        public IList<Expedition> GetAll() {
            throw new NotImplementedException();
        }

        public IList<Expedition> GetByValue(Expedition entity) {
            throw new NotImplementedException();
        }

        public Expedition GetOneById(string id) {
            throw new NotImplementedException();
        }

        public void Save(Expedition entity) {
            throw new NotImplementedException();
        }
    }
}
