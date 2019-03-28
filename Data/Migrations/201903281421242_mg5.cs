namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "approvedBy_idAdmin", "dbo.Admins");
            DropIndex("dbo.Events", new[] { "approvedBy_idAdmin" });
            DropColumn("dbo.Events", "approvedBy_idAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "approvedBy_idAdmin", c => c.Int());
            CreateIndex("dbo.Events", "approvedBy_idAdmin");
            AddForeignKey("dbo.Events", "approvedBy_idAdmin", "dbo.Admins", "idAdmin");
        }
    }
}
