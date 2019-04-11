namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scar1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "date");
        }
    }
}
