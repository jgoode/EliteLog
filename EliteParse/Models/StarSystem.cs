using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteParse.Models {
    /// <summary>
    /// StarSystem Entity
    /// </summary>
    public class StarSystem : IEntity {
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ExpeditionId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double DistToNext { get; set; }
        public int ObjectCount { get; set; }
        public int ScanCount { get; set; }
        public int Counter { get; set; }
        public int StarCount { get; set; }
        public int BlackHoleCount { get; set; }
        public int NeutronStarCount { get; set; }
        public int WhiteDwarfCount { get; set; }
        public int EarthLikeCount { get; set; }
        public int WaterWorldCount { get; set; }
        public int MetalRichCount { get; set; }
        public int HighMetalCount { get; set; }
        public int AmmoniaCount { get; set; }
        public int JovianCount { get; set; }
        public int IcyPlanetCount { get; set; }
        public int RockyCount { get; set; }
        public bool Discovered { get; set; }
        public bool Refuled { get; set; }
        public double DistanceRunningTotal { get; set; }
        public double ScanCountRunningTotal { get; set; }
    }
}
