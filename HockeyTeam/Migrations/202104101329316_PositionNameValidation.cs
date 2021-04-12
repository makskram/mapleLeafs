namespace HockeyTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionNameValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "Name", c => c.String());
        }
    }
}
