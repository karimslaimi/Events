namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mj1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        idAdmin = c.Int(nullable: false, identity: true),
                        nameAdmin = c.String(),
                        mailAdmin = c.String(),
                        passwordAdmin = c.String(),
                        isSuperAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.idAdmin);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        idEvent = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        EventLocation = c.String(),
                        Description = c.String(),
                        approvedBy_idAdmin = c.Int(),
                        creator_id = c.Int(),
                        hostedby_idorg = c.Int(),
                        theme_idTheme = c.Int(),
                    })
                .PrimaryKey(t => t.idEvent)
                .ForeignKey("dbo.Admins", t => t.approvedBy_idAdmin)
                .ForeignKey("dbo.Users", t => t.creator_id)
                .ForeignKey("dbo.organizations", t => t.hostedby_idorg)
                .ForeignKey("dbo.Themes", t => t.theme_idTheme)
                .Index(t => t.approvedBy_idAdmin)
                .Index(t => t.creator_id)
                .Index(t => t.hostedby_idorg)
                .Index(t => t.theme_idTheme);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        firstname = c.String(),
                        lastname = c.String(),
                        mail = c.String(),
                        phone = c.String(),
                        password = c.String(),
                        birthdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
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
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        idTheme = c.Int(nullable: false, identity: true),
                        designation = c.String(),
                    })
                .PrimaryKey(t => t.idTheme);
            
            CreateTable(
                "dbo.Participates",
                c => new
                    {
                        idParticipation = c.Int(nullable: false, identity: true),
                        Event_idEvent = c.Int(),
                    })
                .PrimaryKey(t => t.idParticipation)
                .ForeignKey("dbo.Events", t => t.Event_idEvent)
                .Index(t => t.Event_idEvent);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        idsubs = c.Int(nullable: false, identity: true),
                        mailsubs = c.String(),
                    })
                .PrimaryKey(t => t.idsubs);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participates", "Event_idEvent", "dbo.Events");
            DropForeignKey("dbo.Events", "theme_idTheme", "dbo.Themes");
            DropForeignKey("dbo.Events", "hostedby_idorg", "dbo.organizations");
            DropForeignKey("dbo.organizations", "university_idUniv", "dbo.Universities");
            DropForeignKey("dbo.Events", "creator_id", "dbo.Users");
            DropForeignKey("dbo.Events", "approvedBy_idAdmin", "dbo.Admins");
            DropIndex("dbo.Participates", new[] { "Event_idEvent" });
            DropIndex("dbo.organizations", new[] { "university_idUniv" });
            DropIndex("dbo.Events", new[] { "theme_idTheme" });
            DropIndex("dbo.Events", new[] { "hostedby_idorg" });
            DropIndex("dbo.Events", new[] { "creator_id" });
            DropIndex("dbo.Events", new[] { "approvedBy_idAdmin" });
            DropTable("dbo.Subscribers");
            DropTable("dbo.Participates");
            DropTable("dbo.Themes");
            DropTable("dbo.Universities");
            DropTable("dbo.organizations");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
            DropTable("dbo.Admins");
        }
    }
}
