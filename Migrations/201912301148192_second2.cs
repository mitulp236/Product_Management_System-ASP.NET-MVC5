namespace demo_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Sdescription", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Ldescription", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Simage", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Limage", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Products", "Limage", c => c.String());
            AlterColumn("dbo.Products", "Simage", c => c.String());
            AlterColumn("dbo.Products", "Ldescription", c => c.String());
            AlterColumn("dbo.Products", "Sdescription", c => c.String());
            AlterColumn("dbo.Products", "Category", c => c.String());
            AlterColumn("dbo.Products", "ProductName", c => c.String());
        }
    }
}
