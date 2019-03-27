namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mj1 : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
                "dbo.organizations",
                c => new
                    {
                        idorg = c.Int(nullable: false, identity: true),
                        orgname = c.String(),
                        university_idUniv = c.Int(),
                    })
                .PrimaryKey(t => t.idorg)
                .ForeignKey("dbo.Universities", t => t.university_idUniv)
                .Index(t => t.university_idUniv);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        idUniv = c.Int(nullable: false, identity: true),
                        UnivName = c.String(),
                    })
                .PrimaryKey(t => t.idUniv);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEvents", "User_id", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "Event_idEvent", "dbo.Events");
            DropForeignKey("dbo.Events", "theme_idTheme", "dbo.Themes");
            DropForeignKey("dbo.EventPictures", "picEvent_idEvent", "dbo.Events");
            DropForeignKey("dbo.Events", "hostedby_idorg", "dbo.organizations");
            DropForeignKey("dbo.organizations", "university_idUniv", "dbo.Universities");
            DropForeignKey("dbo.Events", "creator_id", "dbo.Users");
            DropForeignKey("dbo.Events", "approvedBy_idAdmin", "dbo.Admins");
            DropIndex("dbo.UserEvents", new[] { "User_id" });
            DropIndex("dbo.UserEvents", new[] { "Event_idEvent" });
            DropIndex("dbo.EventPictures", new[] { "picEvent_idEvent" });
            DropIndex("dbo.organizations", new[] { "university_idUniv" });
            DropIndex("dbo.Users", new[] { "phone" });
            DropIndex("dbo.Users", new[] { "mail" });
            DropIndex("dbo.Users", new[] { "username" });
            DropIndex("dbo.Events", new[] { "theme_idTheme" });
            DropIndex("dbo.Events", new[] { "hostedby_idorg" });
            DropIndex("dbo.Events", new[] { "creator_id" });
            DropIndex("dbo.Events", new[] { "approvedBy_idAdmin" });
            DropTable("dbo.UserEvents");
            DropTable("dbo.Subscribers");
            DropTable("dbo.Themes");
            DropTable("dbo.EventPictures");
            DropTable("dbo.Universities");
            DropTable("dbo.organizations");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
            DropTable("dbo.Admins");
        }
    }
}
