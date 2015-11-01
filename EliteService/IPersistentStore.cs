using EliteModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteService {
    public interface IPersistentStore {
        Task<Expedition> GetCurrentExpedition();
        Task<IEnumerable<Expedition>> GetAllExpeditions();
        Task<IEnumerable<Expedition>> GetSystemsByExpedition(Expedition expedition);
        Task<StarSystem> AddNewStarSystem(StarSystem starSystem);
        Task<Dictionary<Expedition, List<StarSystem>>> GetExpeditionStarSystems();

    }
}
