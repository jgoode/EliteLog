﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository {
    /// <summary>
    /// Interface for all Repository classes
    /// </summary>
    /// <typeparam name="IEntity">IEntity for mapped entity objects</typeparam>
    public interface IRepository<IEntity> {
        //Task<IEntity> Save(IEntity entity);
        Task<IEntity> Udpate(IEntity entity);
        Task<IEntity> Insert(IEntity entity);
        Task<IEnumerable<IEntity>> GetAll();
        Task<IEntity> GetOneById(string id);
        Task<IEntity> GetByName(string name);
    }
}
