namespace HockeyTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Players", "Description", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "Description", c => c.String());
            AlterColumn("dbo.Players", "Name", c => c.String());
        }
    }
}
