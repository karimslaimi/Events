namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Participates", "Event_idEvent", "dbo.Events");
            DropIndex("dbo.Participates", new[] { "Event_idEvent" });
            CreateTable(
                "dbo.EventPictures",
                c => new
                    {
                        idPicture = c.Int(nullable: false, identity: true),
                        picName = c.String(),
                        picEvent_idEvent = c.Int(),
                    })
                .PrimaryKey(t => t.idPicture)
                .ForeignKey("dbo.Events", t => t.picEvent_idEvent)
                .Index(t => t.picEvent_idEvent);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        idUsev = c.Int(nullable: false, identity: true),
                        participation = c.Boolean(nullable: false),
                        comment = c.String(),
                        Event_idEvent = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.idUsev)
                .ForeignKey("dbo.Events", t => t.Event_idEvent)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Event_idEvent)
                .Index(t => t.User_id);
            
            DropTable("dbo.Participates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Participates",
                c => new
                    {
                        idParticipation = c.Int(nullable: false, identity: true),
                        Event_idEvent = c.Int(),
                    })
                .PrimaryKey(t => t.idParticipation);
            
            DropForeignKey("dbo.UserEvents", "User_id", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "Event_idEvent", "dbo.Events");
            DropForeignKey("dbo.EventPictures", "picEvent_idEvent", "dbo.Events");
            DropIndex("dbo.UserEvents", new[] { "User_id" });
            DropIndex("dbo.UserEvents", new[] { "Event_idEvent" });
            DropIndex("dbo.EventPictures", new[] { "picEvent_idEvent" });
            DropTable("dbo.UserEvents");
            DropTable("dbo.EventPictures");
            CreateIndex("dbo.Participates", "Event_idEvent");
            AddForeignKey("dbo.Participates", "Event_idEvent", "dbo.Events", "idEvent");
        }
    }
}
