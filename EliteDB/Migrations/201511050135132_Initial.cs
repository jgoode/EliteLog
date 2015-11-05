namespace EliteDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expeditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObjectId = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        StartSystem = c.String(),
                        EndSystem = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Profit = c.Double(nullable: false),
                        TotalDistance = c.Double(nullable: false),
                        Current = c.Boolean(nullable: false),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StarSystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObjectId = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Z = c.Double(nullable: false),
                        DistToNext = c.Double(nullable: false),
                        ObjectCount = c.Int(nullable: false),
                        ScanCount = c.Int(nullable: false),
                        Counter = c.Int(nullable: false),
                        StarCount = c.Int(nullable: false),
                        BlackHoleCount = c.Int(nullable: false),
                        NeutronStarCount = c.Int(nullable: false),
                        WhiteDwarfCount = c.Int(nullable: false),
                        EarthLikeCount = c.Int(nullable: false),
                        WaterWorldCount = c.Int(nullable: false),
                        MetalRichCount = c.Int(nullable: false),
                        HighMetalCount = c.Int(nullable: false),
                        AmmoniaCount = c.Int(nullable: false),
                        JovianCount = c.Int(nullable: false),
                        IcyPlanetCount = c.Int(nullable: false),
                        RockyCount = c.Int(nullable: false),
                        Discovered = c.Boolean(nullable: false),
                        Refuled = c.Boolean(nullable: false),
                        DistanceRunningTotal = c.Double(nullable: false),
                        ScanCountRunningTotal = c.Double(nullable: false),
                        Expedition_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Expeditions", t => t.Expedition_Id, cascadeDelete: true)
                .Index(t => t.Expedition_Id);
            
            CreateTable(
                "dbo.StarSystemObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObjectType = c.String(),
                        StarSystem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StarSystems", t => t.StarSystem_Id, cascadeDelete: true)
                .Index(t => t.StarSystem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StarSystemObjects", "StarSystem_Id", "dbo.StarSystems");
            DropForeignKey("dbo.StarSystems", "Expedition_Id", "dbo.Expeditions");
            DropIndex("dbo.StarSystemObjects", new[] { "StarSystem_Id" });
            DropIndex("dbo.StarSystems", new[] { "Expedition_Id" });
            DropTable("dbo.StarSystemObjects");
            DropTable("dbo.StarSystems");
            DropTable("dbo.Expeditions");
        }
    }
}
