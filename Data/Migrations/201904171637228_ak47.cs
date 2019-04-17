namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ak47 : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.EventPictures",
                c => new
                    {
                        idPicture = c.Int(nullable: false, identity: true),
                        picName = c.String(),
                        eventid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idPicture)
                .ForeignKey("dbo.Events", t => t.eventid, cascadeDelete: true)
                .Index(t => t.eventid);
            
           
        }
        
        public override void Down()
        {
        }
    }
}
