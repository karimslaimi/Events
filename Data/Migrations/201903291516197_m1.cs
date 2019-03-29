namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Users", "username", unique: true);
            CreateIndex("dbo.Users", "mail", unique: true);
            CreateIndex("dbo.Users", "phone", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "phone" });
            DropIndex("dbo.Users", new[] { "mail" });
            DropIndex("dbo.Users", new[] { "username" });
        }
    }
}
