namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class like1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserEvents", "like", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserEvents", "like", c => c.Int());
        }
    }
}
