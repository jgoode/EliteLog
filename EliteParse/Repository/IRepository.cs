using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace EliteParse.Repository {
    /// <summary>
    /// Interface for all Repository classes
    /// </summary>
    /// <typeparam name="IEntity">IEntity for mapped Parse objects</typeparam>
    public interface IRepository<IEntity> {
        Task<IEntity> Save(IEntity entity);
        Task<IList<IEntity>> GetAll();
        Task<IEntity> GetOneById(string id);
        Task<IEntity> GetByName(string name);
    }
}
