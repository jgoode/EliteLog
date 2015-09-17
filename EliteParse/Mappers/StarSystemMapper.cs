using EliteParse.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Mappers {
    public class StarSystemMapper {
        public static StarSystem Map(ParseObject parse) {
            return new StarSystem {
                AmmoniaCount = parse.Get<int>("ammoniaCount"),
                BlackHoleCount = parse.Get<int>("blackHoleCount"),
                Counter = parse.Get<int>("counter"),
                CreatedAt = parse.CreatedAt.Value,
                Discovered = parse.Get<bool>("discovered"),
                DistanceRunningTotal = parse.Get<double>("distanceRT"),
                DistToNext = parse.Get<double>("distToNext"),
                EarthLikeCount = parse.Get<int>("earthLikeCount"),
                ExpeditionId = parse.Get<string>("expedition"),
                HighMetalCount = parse.Get<int>("highMetalCount"),
                IcyPlanetCount = parse.Get<int>("icyPlanetCount"),
                JovianCount = parse.Get<int>("jovianCount"),
                MetalRichCount = parse.Get<int>("MetalRichCount"),
                Name = parse.Get<string>("name"),
                NeutronStarCount = parse.Get<int>("neutronStarCount"),
                ObjectCount  = parse.Get<int>("objectCount"),
                ObjectId = parse.ObjectId,
                Refuled = parse.Get<bool>("refuel"),
                RockyCount = parse.Get<int>("rockyCount"),
                ScanCount = parse.Get<int>("scanCount"),
                ScanCountRunningTotal = parse.Get<double>("scanCountRT"),
                StarCount = parse.Get<int>("starCount"),
                UpdatedAt = parse.UpdatedAt.Value,
                WaterWorldCount = parse.Get<int>("waterWorldCount"),
                WhiteDwarfCount = parse.Get<int>("whiteDwarfCount"),
                X = parse.Get<double>("x"),
                Y = parse.Get<double>("y"),
                Z = parse.Get<double>("z")
            };
        }
    }
}
