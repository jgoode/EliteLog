using EliteParse.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Repository {
    public interface ISystemPointerRepository : IRepository<SystemPointer> {
        Task<ParseObject> GetCurrent();
        Task<ParseObject> Save(ParseObject po);
    }
}
