using EliteModels;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Mappers {
    /// <summary>
    /// Maps Parse System object to StarSystem object
    /// </summary>
    public class StarSystemMapper {
        /// <summary>
        /// Returns a StarSystem instance
        /// </summary>
        /// <param name="parse"></param>
        /// <returns></returns>
        public static StarSystem Map(ParseObject parse) {
            var starSystem = new StarSystem();
            if (parse.Keys.Contains("ammoniaCount"))
                starSystem.AmmoniaCount = parse["ammoniaCount"] == null ? 0 : parse.Get<int>("ammoniaCount");
            if (parse.Keys.Contains("blackHoleCount"))
                starSystem.BlackHoleCount = parse["blackHoleCount"] == null ? 0 : parse.Get<int>("blackHoleCount");
            if (parse.Keys.Contains("counter"))
                starSystem.Counter = parse["counter"] == null ? 0 : parse.Get<int>("counter");
            starSystem.CreatedAt = parse.CreatedAt.Value;
            if (parse.Keys.Contains("discovered"))
                starSystem.Discovered = parse.Get<bool>("discovered");
            if (parse.Keys.Contains("distanceRT"))
                starSystem.DistanceRunningTotal = parse["distanceRT"] == null ? 0 : parse.Get<double>("distanceRT");
            if (parse.Keys.Contains("distToNext"))
                starSystem.DistToNext = parse["distToNext"] == null ? 0 : parse.Get<double>("distToNext");
            if (parse.Keys.Contains("earthLikeCount"))
                starSystem.EarthLikeCount = parse["earthLikeCount"] == null ? 0 : parse.Get<int>("earthLikeCount");
            //ExpeditionId = parse.Get<string>("expedition"),
            if (parse.Keys.Contains("highMetalCount"))
                starSystem.HighMetalCount = parse["highMetalCount"] == null ? 0 : parse.Get<int>("highMetalCount");
            if (parse.Keys.Contains("icyPlanetCount"))
                starSystem.IcyPlanetCount = parse["icyPlanetCount"] == null ? 0 : parse.Get<int>("icyPlanetCount");
            if (parse.Keys.Contains("jovianCount"))
                starSystem.JovianCount = parse["jovianCount"] == null ? 0 : parse.Get<int>("jovianCount");
            if (parse.Keys.Contains("MetalRichCount"))
                starSystem.MetalRichCount = parse["MetalRichCount"] == null ? 0 : parse.Get<int>("MetalRichCount");
            if (parse.Keys.Contains("name"))
                starSystem.Name = parse.Get<string>("name");
            if (parse.Keys.Contains("neutronStarCount"))
                starSystem.NeutronStarCount = parse["neutronStarCount"] == null ? 0 : parse.Get<int>("neutronStarCount");
            if (parse.Keys.Contains("objectCount"))
                starSystem.ObjectCount = parse["objectCount"] == null ? 0 : parse.Get<int>("objectCount");

            starSystem.ObjectId = parse.ObjectId;
            if (parse.Keys.Contains("refuel"))
                starSystem.Refuled = parse.Get<bool>("refuel");
            if (parse.Keys.Contains("rockyCount"))
                starSystem.RockyCount = parse["rockyCount"] == null ? 0 : parse.Get<int>("rockyCount");
            if (parse.Keys.Contains("scanCount"))
                starSystem.ScanCount = parse["scanCount"] == null ? 0 : parse.Get<int>("scanCount");
            if (parse.Keys.Contains("scanCountRT"))
                starSystem.ScanCountRunningTotal = parse["scanCountRT"] == null ? 0 : parse.Get<double>("scanCountRT");
            if (parse.Keys.Contains("starCount"))
                starSystem.StarCount = parse["starCount"] == null ? 0 : parse.Get<int>("starCount");

            starSystem.UpdatedAt = parse.UpdatedAt.Value;
            if (parse.Keys.Contains("waterWorldCount"))
                starSystem.WaterWorldCount = parse["waterWorldCount"] == null ? 0 : parse.Get<int>("waterWorldCount");
            if (parse.Keys.Contains("whiteDwarfCount"))
                starSystem.WhiteDwarfCount = parse["whiteDwarfCount"] == null ? 0 : parse.Get<int>("whiteDwarfCount");
            if (parse.Keys.Contains("x"))
                starSystem.X = parse["x"] == null ? 0 : parse.Get<double>("x");
            if (parse.Keys.Contains("y"))
                starSystem.Y = parse["y"] == null ? 0 : parse.Get<double>("y");
            if (parse.Keys.Contains("z"))
                starSystem.Z = parse["z"] == null ? 0 : parse.Get<double>("z");
            return starSystem;
        }
    }
}
