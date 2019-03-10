namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "nameAdmin", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "mailAdmin", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "passwordAdmin", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "passwordAdmin", c => c.String());
            AlterColumn("dbo.Admins", "mailAdmin", c => c.String());
            AlterColumn("dbo.Admins", "nameAdmin", c => c.String());
        }
    }
}
