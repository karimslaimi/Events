namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scar4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "activated", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "activated");
        }
    }
}
