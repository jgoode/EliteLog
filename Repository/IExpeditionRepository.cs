using EliteModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository {
    public interface IExpeditionRepository : IRepository<Expedition> {
        Task<Expedition> GetCurrent(string user);
    }
}
