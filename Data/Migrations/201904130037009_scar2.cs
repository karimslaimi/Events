namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scar2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "mail" });
            AlterColumn("dbo.Users", "mail", c => c.String(maxLength: 254));
            CreateIndex("dbo.Users", "mail", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "mail" });
            AlterColumn("dbo.Users", "mail", c => c.String(maxLength: 20));
            CreateIndex("dbo.Users", "mail", unique: true);
        }
    }
}
