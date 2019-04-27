namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class like : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEvents", "like", c => c.Int());
            DropColumn("dbo.UserEvents", "star");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEvents", "star", c => c.Int());
            DropColumn("dbo.UserEvents", "like");
        }
    }
}
