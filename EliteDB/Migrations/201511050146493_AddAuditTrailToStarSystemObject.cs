namespace EliteDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditTrailToStarSystemObject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StarSystemObjects", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.StarSystemObjects", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StarSystemObjects", "UpdatedAt");
            DropColumn("dbo.StarSystemObjects", "CreatedAt");
        }
    }
}
