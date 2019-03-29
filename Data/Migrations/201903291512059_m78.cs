namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m78 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Events", "creatorid");
            AddForeignKey("dbo.Events", "creatorid", "dbo.Users", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "creatorid", "dbo.Users");
            DropIndex("dbo.Events", new[] { "creatorid" });
        }
    }
}
