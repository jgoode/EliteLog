using EliteModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository {
    public interface IStarSystemRepository : IRepository<StarSystem> {
        Task<IEnumerable<StarSystem>> GetByExpedition(Expedition expedition);
    }
}
