using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace EliteParse.Repository {
    public interface IRepository<IEntity> {
        void Save(IEntity entity);
        IList<IEntity> GetAll();
        IEntity GetOneById(string id);
        IList<IEntity> GetByValue(IEntity entity);
    }
}
