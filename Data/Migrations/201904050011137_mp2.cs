namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mp2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "creatorid", "dbo.Users");
            DropForeignKey("dbo.Events", "hostedbyid", "dbo.organizations");
            DropForeignKey("dbo.Events", "themeid", "dbo.Themes");
            DropIndex("dbo.Events", new[] { "hostedbyid" });
            DropIndex("dbo.Events", new[] { "creatorid" });
            DropIndex("dbo.Events", new[] { "themeid" });
            AlterColumn("dbo.Events", "hostedbyid", c => c.Int());
            AlterColumn("dbo.Events", "creatorid", c => c.Int());
            AlterColumn("dbo.Events", "themeid", c => c.Int());
            CreateIndex("dbo.Events", "hostedbyid");
            CreateIndex("dbo.Events", "creatorid");
            CreateIndex("dbo.Events", "themeid");
            AddForeignKey("dbo.Events", "creatorid", "dbo.Users", "id");
            AddForeignKey("dbo.Events", "hostedbyid", "dbo.organizations", "idorg");
            AddForeignKey("dbo.Events", "themeid", "dbo.Themes", "idTheme");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "themeid", "dbo.Themes");
            DropForeignKey("dbo.Events", "hostedbyid", "dbo.organizations");
            DropForeignKey("dbo.Events", "creatorid", "dbo.Users");
            DropIndex("dbo.Events", new[] { "themeid" });
            DropIndex("dbo.Events", new[] { "creatorid" });
            DropIndex("dbo.Events", new[] { "hostedbyid" });
            AlterColumn("dbo.Events", "themeid", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "creatorid", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "hostedbyid", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "themeid");
            CreateIndex("dbo.Events", "creatorid");
            CreateIndex("dbo.Events", "hostedbyid");
            AddForeignKey("dbo.Events", "themeid", "dbo.Themes", "idTheme", cascadeDelete: true);
            AddForeignKey("dbo.Events", "hostedbyid", "dbo.organizations", "idorg", cascadeDelete: true);
            AddForeignKey("dbo.Events", "creatorid", "dbo.Users", "id", cascadeDelete: true);
        }
    }
}
