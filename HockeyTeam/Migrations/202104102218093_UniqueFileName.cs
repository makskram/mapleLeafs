namespace HockeyTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueFileName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlayerImages", "FileName", c => c.String(maxLength: 100));
            CreateIndex("dbo.PlayerImages", "FileName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlayerImages", new[] { "FileName" });
            AlterColumn("dbo.PlayerImages", "FileName", c => c.String());
        }
    }
}
