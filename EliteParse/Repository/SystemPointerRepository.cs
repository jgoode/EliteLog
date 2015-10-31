﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteParse.Models;
using Parse;
using EliteParse.Mappers;

namespace EliteParse.Repository {
    public class SystemPointerRepository : ISystemPointerRepository {
        private Expedition _expedition;

        public SystemPointerRepository(Expedition expedition) {
            _expedition = expedition;
        }

        public async Task<IList<SystemPointer>> GetAll() {
            var query = from item in ParseObject.GetQuery("SystemPointer")
                        where  item.Get<string>("expedition") == _expedition.ObjectId
                        select item;

            var items = await query.FindAsync();
            IEnumerable<SystemPointer> pointers = items.ToList().Select(p => SystemPointerMapper.Map(p));
            return pointers.ToList();
        }

        public async Task<ParseObject> GetCurrent() {
            var query = from item in ParseObject.GetQuery("SystemPointer")
                        where item.Get<string>("expedition") == _expedition.ObjectId
                        select item;
            ParseObject po = await query.FirstAsync();
            return po;
        }

        public async Task<ParseObject> Save(ParseObject po) {
            await po.SaveAsync();
            return po;
        }

        public async Task<SystemPointer> GetByName(string name) {
            throw new NotImplementedException();
        }

        public async Task<SystemPointer> GetOneById(string id) {
            throw new NotImplementedException();
        }

        public async Task<SystemPointer> Save(SystemPointer entity) {
            if (null == entity) throw new ArgumentNullException("SystemPointer");
            ParseObject systemPointer = new ParseObject("SystemPointer");
            
            systemPointer["expedition"] = _expedition.ObjectId;
            systemPointer["currentObjectId"] = entity.CurrentObjectId;
            systemPointer["lastObjectId"] = entity.LastObjectId;
            await systemPointer.SaveAsync();
            entity.ObjectId = systemPointer.ObjectId;
            entity.CreatedAt = systemPointer.CreatedAt.Value;
            entity.UpdatedAt = systemPointer.UpdatedAt.Value;
            return entity;
        }
    }
}