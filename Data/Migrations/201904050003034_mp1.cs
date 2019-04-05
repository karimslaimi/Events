namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mp1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "hostedby_idorg", "dbo.organizations");
            DropForeignKey("dbo.Events", "theme_idTheme", "dbo.Themes");
            DropForeignKey("dbo.organizations", "university_idUniv", "dbo.Universities");
            DropForeignKey("dbo.EventPictures", "picEvent_idEvent", "dbo.Events");
            DropIndex("dbo.Events", new[] { "hostedby_idorg" });
            DropIndex("dbo.Events", new[] { "theme_idTheme" });
            DropIndex("dbo.organizations", new[] { "university_idUniv" });
            DropIndex("dbo.EventPictures", new[] { "picEvent_idEvent" });
            RenameColumn(table: "dbo.Events", name: "hostedby_idorg", newName: "hostedbyid");
            RenameColumn(table: "dbo.Events", name: "theme_idTheme", newName: "themeid");
            RenameColumn(table: "dbo.organizations", name: "university_idUniv", newName: "idUniv");
            RenameColumn(table: "dbo.EventPictures", name: "picEvent_idEvent", newName: "eventid");
            AlterColumn("dbo.Events", "hostedbyid", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "themeid", c => c.Int(nullable: false));
            AlterColumn("dbo.organizations", "idUniv", c => c.Int(nullable: false));
            AlterColumn("dbo.EventPictures", "eventid", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "hostedbyid");
            CreateIndex("dbo.Events", "themeid");
            CreateIndex("dbo.organizations", "idUniv");
            CreateIndex("dbo.EventPictures", "eventid");
            AddForeignKey("dbo.Events", "hostedbyid", "dbo.organizations", "idorg", cascadeDelete: true);
            AddForeignKey("dbo.Events", "themeid", "dbo.Themes", "idTheme", cascadeDelete: true);
            AddForeignKey("dbo.organizations", "idUniv", "dbo.Universities", "idUniv", cascadeDelete: true);
            AddForeignKey("dbo.EventPictures", "eventid", "dbo.Events", "idEvent", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventPictures", "eventid", "dbo.Events");
            DropForeignKey("dbo.organizations", "idUniv", "dbo.Universities");
            DropForeignKey("dbo.Events", "themeid", "dbo.Themes");
            DropForeignKey("dbo.Events", "hostedbyid", "dbo.organizations");
            DropIndex("dbo.EventPictures", new[] { "eventid" });
            DropIndex("dbo.organizations", new[] { "idUniv" });
            DropIndex("dbo.Events", new[] { "themeid" });
            DropIndex("dbo.Events", new[] { "hostedbyid" });
            AlterColumn("dbo.EventPictures", "eventid", c => c.Int());
            AlterColumn("dbo.organizations", "idUniv", c => c.Int());
            AlterColumn("dbo.Events", "themeid", c => c.Int());
            AlterColumn("dbo.Events", "hostedbyid", c => c.Int());
            RenameColumn(table: "dbo.EventPictures", name: "eventid", newName: "picEvent_idEvent");
            RenameColumn(table: "dbo.organizations", name: "idUniv", newName: "university_idUniv");
            RenameColumn(table: "dbo.Events", name: "themeid", newName: "theme_idTheme");
            RenameColumn(table: "dbo.Events", name: "hostedbyid", newName: "hostedby_idorg");
            CreateIndex("dbo.EventPictures", "picEvent_idEvent");
            CreateIndex("dbo.organizations", "university_idUniv");
            CreateIndex("dbo.Events", "theme_idTheme");
            CreateIndex("dbo.Events", "hostedby_idorg");
            AddForeignKey("dbo.EventPictures", "picEvent_idEvent", "dbo.Events", "idEvent");
            AddForeignKey("dbo.organizations", "university_idUniv", "dbo.Universities", "idUniv");
            AddForeignKey("dbo.Events", "theme_idTheme", "dbo.Themes", "idTheme");
            AddForeignKey("dbo.Events", "hostedby_idorg", "dbo.organizations", "idorg");
        }
    }
}
