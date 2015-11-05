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
        private CurrentSystem _currentSystem;
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
        
        public async Task<CurrentSystem> AddNewStarSystem(SystemPosition ps) {
            if (null == _currentSystem) {
                var exp = await GetCurrentExpedition();
            } 

            var ss = new StarSystem();
            ss.Name = ps.Name;
            _currentSystem.StarSystem = ss;

            return _currentSystem;

            //var sys = await _starSystemRepository.Save(ss);
            ////LogText(string.Format("{0}: Adding system: {1}", DateTime.Now, ps.Name));
            //if (_currentSystem.SystemPointer == null) {

            //} else {

            //}
            //var current = _systemPointer.Get<string>("currentObjectId"); ;
            //_systemPointer["lastObjectId"] = current;
            //_systemPointer["currentObjectId"] = sys.ObjectId;
            //_systemPointer = await _systemPointerRepository.Save(_systemPointer);

        }

        public async Task<IEnumerable<Expedition>> GetAllExpeditions() {
            throw new NotImplementedException();
        }

        public async Task<Expedition> GetCurrentExpedition() {
            Expedition currentExpedition = await _expeditionRepository.GetCurrent(_user.UserName);
            if (_currentSystem == null) {
                _currentSystem = new CurrentSystem();
                _currentSystem.Expedition = currentExpedition;
            }
            return currentExpedition;
        }

        public async Task<IEnumerable<Expedition>> GetSystemsByExpedition(Expedition expedition) {
            throw new NotImplementedException();
        }

        public async Task<Expedition> SaveExpedition(Expedition expedition) {
            return await _expeditionRepository.Save(expedition);
        }
    }
}
