namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "username", c => c.String(maxLength: 20));
            AlterColumn("dbo.Users", "firstname", c => c.String(maxLength: 15));
            AlterColumn("dbo.Users", "lastname", c => c.String(maxLength: 15));
            AlterColumn("dbo.Users", "mail", c => c.String(maxLength: 20));
            AlterColumn("dbo.Users", "phone", c => c.String(maxLength: 8));
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Users", "username", unique: true);
            CreateIndex("dbo.Users", "mail", unique: true);
            CreateIndex("dbo.Users", "phone", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "phone" });
            DropIndex("dbo.Users", new[] { "mail" });
            DropIndex("dbo.Users", new[] { "username" });
            AlterColumn("dbo.Users", "password", c => c.String());
            AlterColumn("dbo.Users", "phone", c => c.String());
            AlterColumn("dbo.Users", "mail", c => c.String());
            AlterColumn("dbo.Users", "lastname", c => c.String());
            AlterColumn("dbo.Users", "firstname", c => c.String());
            AlterColumn("dbo.Users", "username", c => c.String());
        }
    }
}
