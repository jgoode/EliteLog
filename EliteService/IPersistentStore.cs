﻿using EliteModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteService {
    public interface IPersistentStore {

        Expedition CurrentExpedition { get; }
        SystemPointer SystPointer { get; set; }
        IEnumerable<StarSystem> StarSystems { get; set; }

        Task<Expedition> GetCurrentExpedition();
        Task<IEnumerable<Expedition>> GetAllExpeditions();
        Task<IEnumerable<StarSystem>> GetSystemsByExpedition(Expedition expedition);
        Task<StarSystem> AddNewStarSystem(SystemPosition ps);
        Task ClearExpeditionCurrentFlags();
        Task<Expedition> UpdateExpedition(Expedition expedition);
        Task<Expedition> GetByExpeditionName(string name);
        Task<Expedition> InsertExpedition(Expedition expedition);
        Task SetCurrentExpedition(Expedition expedition);
    }
}
