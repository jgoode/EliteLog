using EliteParse.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteService {
    public interface IParsePersistentStore : IPersistentStore {
        IExpeditionRepository ExpeditionRepository { get;  }
        ISystemPointerRepository SystemPointerRepository { get;  }
        IStarSystemRepository StarSystemRepository { get;  }
    }
}
