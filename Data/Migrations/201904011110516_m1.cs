namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {

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
                    creatorid = c.Int(nullable: false),
                    approvedBy_idAdmin = c.Int(),
                    hostedby_idorg = c.Int(),
                    theme_idTheme = c.Int(),
                })
                .PrimaryKey(t => t.idEvent)
                .ForeignKey("dbo.Admins", t => t.approvedBy_idAdmin)
                .ForeignKey("dbo.Users", t => t.creatorid, cascadeDelete: true)
                .ForeignKey("dbo.organizations", t => t.hostedby_idorg)
                .ForeignKey("dbo.Themes", t => t.theme_idTheme)
                .Index(t => t.creatorid)
                .Index(t => t.approvedBy_idAdmin)
                .Index(t => t.hostedby_idorg)
                .Index(t => t.theme_idTheme);
        }
            
            
            
            
        
        public override void Down()
        {
            
        }
    }
}
