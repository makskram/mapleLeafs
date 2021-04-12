namespace HockeyTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePlayerImageMappings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayerImageMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageNumber = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                        PlayerImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .ForeignKey("dbo.PlayerImages", t => t.PlayerImageID, cascadeDelete: true)
                .Index(t => t.PlayerID)
                .Index(t => t.PlayerImageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerImageMappings", "PlayerImageID", "dbo.PlayerImages");
            DropForeignKey("dbo.PlayerImageMappings", "PlayerID", "dbo.Players");
            DropIndex("dbo.PlayerImageMappings", new[] { "PlayerImageID" });
            DropIndex("dbo.PlayerImageMappings", new[] { "PlayerID" });
            DropTable("dbo.PlayerImageMappings");
        }
    }
}
