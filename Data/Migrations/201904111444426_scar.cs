namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.organizations", "idUniv", "dbo.Universities");
            DropIndex("dbo.organizations", new[] { "idUniv" });
            RenameColumn(table: "dbo.Events", name: "approvedBy_idAdmin", newName: "adminid");
            RenameIndex(table: "dbo.Events", name: "IX_approvedBy_idAdmin", newName: "IX_adminid");
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        logid = c.Int(nullable: false, identity: true),
                        adminid = c.Int(nullable: false),
                        eventid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.logid)
                .ForeignKey("dbo.Admins", t => t.adminid, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.eventid, cascadeDelete: true)
                .Index(t => t.adminid)
                .Index(t => t.eventid);
            
            AddColumn("dbo.UserEvents", "star", c => c.Int(nullable: false));
            AlterColumn("dbo.organizations", "idUniv", c => c.Int(nullable: false));
            CreateIndex("dbo.organizations", "idUniv");
            AddForeignKey("dbo.organizations", "idUniv", "dbo.Universities", "idUniv", cascadeDelete: true);
            DropColumn("dbo.Users", "firstname");
            DropColumn("dbo.Users", "lastname");
            DropColumn("dbo.Users", "birthdate");
            DropColumn("dbo.UserEvents", "comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEvents", "comment", c => c.String());
            AddColumn("dbo.Users", "birthdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "lastname", c => c.String(maxLength: 15));
            AddColumn("dbo.Users", "firstname", c => c.String(maxLength: 15));
            DropForeignKey("dbo.organizations", "idUniv", "dbo.Universities");
            DropForeignKey("dbo.Logs", "eventid", "dbo.Events");
            DropForeignKey("dbo.Logs", "adminid", "dbo.Admins");
            DropIndex("dbo.Logs", new[] { "eventid" });
            DropIndex("dbo.Logs", new[] { "adminid" });
            DropIndex("dbo.organizations", new[] { "idUniv" });
            AlterColumn("dbo.organizations", "idUniv", c => c.Int());
            DropColumn("dbo.UserEvents", "star");
            DropTable("dbo.Logs");
            RenameIndex(table: "dbo.Events", name: "IX_adminid", newName: "IX_approvedBy_idAdmin");
            RenameColumn(table: "dbo.Events", name: "adminid", newName: "approvedBy_idAdmin");
            CreateIndex("dbo.organizations", "idUniv");
            AddForeignKey("dbo.organizations", "idUniv", "dbo.Universities", "idUniv");
        }
    }
}
