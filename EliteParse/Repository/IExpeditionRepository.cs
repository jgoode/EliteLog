﻿using EliteParse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Repository {
    public interface IExpeditionRepository : IRepository<Expedition> {
        Task<Expedition> GetCurrent();
    }
}
