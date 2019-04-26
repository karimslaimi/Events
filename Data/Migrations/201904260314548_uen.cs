namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserEvents", "star", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserEvents", "star", c => c.Int(nullable: false));
        }
    }
}
