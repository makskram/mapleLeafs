namespace HockeyTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Score", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Players", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Players", "Score");
        }
    }
}
