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
        Task<CurrentSystem> AddNewStarSystem(SystemPosition ps);
        Task<Dictionary<Expedition, List<StarSystem>>> GetExpeditionStarSystems();

    }
}
