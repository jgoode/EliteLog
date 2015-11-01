using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteModels;
using EliteParse.Repository;
using Repository;

namespace EliteService {
    public class ParsePersistentStore : IParsePersistentStore {
        private IExpeditionRepository _expeditionRepository;
        private IStarSystemRepository _starSystemRepository;
        private ISystemPointerRepository _systemPointerRepository;
        private Expedition _currentExpedition;
        private User _user;

        public ParsePersistentStore(User user) {
            _expeditionRepository = new ExpeditionRespository();
            _starSystemRepository = new StarSystemRepository();
            _systemPointerRepository = new SystemPointerRepository();
            _user = user;
          
        }

        public IExpeditionRepository ExpeditionRepository {
            get { return _expeditionRepository; }
        }

        public IStarSystemRepository StarSystemRepository {
            get { return _starSystemRepository; }
        }

        public ISystemPointerRepository SystemPointerRepository {
            get { return _systemPointerRepository; }
        }

        public async Task<Dictionary<Expedition, List<StarSystem>>> GetExpeditionStarSystems() {
            throw new NotImplementedException();
        }
        
        public async Task<StarSystem> AddNewStarSystem(StarSystem starSystem) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Expedition>> GetAllExpeditions() {
            throw new NotImplementedException();
        }

        public async Task<Expedition> GetCurrentExpedition() {
            _currentExpedition = await _expeditionRepository.GetCurrent(_user.UserName);
            return _currentExpedition;
        }

        public async Task<IEnumerable<Expedition>> GetSystemsByExpedition(Expedition expedition) {
            throw new NotImplementedException();
        }
    }
}
