namespace HockeyTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PositionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Positions", t => t.PositionID)
                .Index(t => t.PositionID);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "PositionID", "dbo.Positions");
            DropIndex("dbo.Players", new[] { "PositionID" });
            DropTable("dbo.Positions");
            DropTable("dbo.Players");
        }
    }
}
