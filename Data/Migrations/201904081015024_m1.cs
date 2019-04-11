namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            
            
            
            
            CreateTable(
                "dbo.organizations",
                c => new
                    {
                        idorg = c.Int(nullable: false, identity: true),
                        orgname = c.String(),
                        idUniv = c.Int(),
                    })
                .PrimaryKey(t => t.idorg)
                .ForeignKey("dbo.Universities", t => t.idUniv)
                .Index(t => t.idUniv);
            
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
          
        }
    }
}
