namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mp40 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserEvents", "Event_idEvent", "dbo.Events");
            DropForeignKey("dbo.UserEvents", "User_id", "dbo.Users");
            DropIndex("dbo.UserEvents", new[] { "Event_idEvent" });
            DropIndex("dbo.UserEvents", new[] { "User_id" });
            RenameColumn(table: "dbo.UserEvents", name: "Event_idEvent", newName: "eventid");
            RenameColumn(table: "dbo.UserEvents", name: "User_id", newName: "userid");
            AlterColumn("dbo.UserEvents", "eventid", c => c.Int(nullable: false));
            AlterColumn("dbo.UserEvents", "userid", c => c.Int(nullable: false));
            CreateIndex("dbo.UserEvents", "userid");
            CreateIndex("dbo.UserEvents", "eventid");
            AddForeignKey("dbo.UserEvents", "eventid", "dbo.Events", "idEvent", cascadeDelete: true);
            AddForeignKey("dbo.UserEvents", "userid", "dbo.Users", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEvents", "userid", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "eventid", "dbo.Events");
            DropIndex("dbo.UserEvents", new[] { "eventid" });
            DropIndex("dbo.UserEvents", new[] { "userid" });
            AlterColumn("dbo.UserEvents", "userid", c => c.Int());
            AlterColumn("dbo.UserEvents", "eventid", c => c.Int());
            RenameColumn(table: "dbo.UserEvents", name: "userid", newName: "User_id");
            RenameColumn(table: "dbo.UserEvents", name: "eventid", newName: "Event_idEvent");
            CreateIndex("dbo.UserEvents", "User_id");
            CreateIndex("dbo.UserEvents", "Event_idEvent");
            AddForeignKey("dbo.UserEvents", "User_id", "dbo.Users", "id");
            AddForeignKey("dbo.UserEvents", "Event_idEvent", "dbo.Events", "idEvent");
        }
    }
}
