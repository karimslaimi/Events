namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        logid = c.Int(nullable: false, identity: true),
                        adminid = c.Int(nullable: false),
                        eventid = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.logid)
                .ForeignKey("dbo.Admins", t => t.adminid, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.eventid, cascadeDelete: true)
                .Index(t => t.adminid)
                .Index(t => t.eventid);
            
            AddColumn("dbo.Users", "activated", c => c.String());
            AddColumn("dbo.UserEvents", "star", c => c.Int(nullable: false));
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
            DropForeignKey("dbo.Logs", "eventid", "dbo.Events");
            DropForeignKey("dbo.Logs", "adminid", "dbo.Admins");
            DropIndex("dbo.Logs", new[] { "eventid" });
            DropIndex("dbo.Logs", new[] { "adminid" });
            DropColumn("dbo.UserEvents", "star");
            DropColumn("dbo.Users", "activated");
            DropTable("dbo.Logs");
        }
    }
}
