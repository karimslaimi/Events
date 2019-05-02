namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserEvents", "Event_idEvent", "dbo.Events");
            DropForeignKey("dbo.UserEvents", "User_id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "mail" });
            DropIndex("dbo.UserEvents", new[] { "Event_idEvent" });
            DropIndex("dbo.UserEvents", new[] { "User_id" });
            RenameColumn(table: "dbo.UserEvents", name: "Event_idEvent", newName: "eventid");
            RenameColumn(table: "dbo.UserEvents", name: "User_id", newName: "userid");
            AddColumn("dbo.UserEvents", "like", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "mail", c => c.String(maxLength: 254));
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
            AlterColumn("dbo.UserEvents", "eventid", c => c.Int(nullable: false));
            AlterColumn("dbo.UserEvents", "userid", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "mail", unique: true);
            CreateIndex("dbo.UserEvents", "userid");
            CreateIndex("dbo.UserEvents", "eventid");
            AddForeignKey("dbo.UserEvents", "eventid", "dbo.Events", "idEvent", cascadeDelete: true);
            AddForeignKey("dbo.UserEvents", "userid", "dbo.Users", "id", cascadeDelete: true);
            DropColumn("dbo.UserEvents", "star");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEvents", "star", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserEvents", "userid", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "eventid", "dbo.Events");
            DropIndex("dbo.UserEvents", new[] { "eventid" });
            DropIndex("dbo.UserEvents", new[] { "userid" });
            DropIndex("dbo.Users", new[] { "mail" });
            AlterColumn("dbo.UserEvents", "userid", c => c.Int());
            AlterColumn("dbo.UserEvents", "eventid", c => c.Int());
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "mail", c => c.String(maxLength: 20));
            DropColumn("dbo.UserEvents", "like");
            RenameColumn(table: "dbo.UserEvents", name: "userid", newName: "User_id");
            RenameColumn(table: "dbo.UserEvents", name: "eventid", newName: "Event_idEvent");
            CreateIndex("dbo.UserEvents", "User_id");
            CreateIndex("dbo.UserEvents", "Event_idEvent");
            CreateIndex("dbo.Users", "mail", unique: true);
            AddForeignKey("dbo.UserEvents", "User_id", "dbo.Users", "id");
            AddForeignKey("dbo.UserEvents", "Event_idEvent", "dbo.Events", "idEvent");
        }
    }
}
