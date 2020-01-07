namespace demo_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Simage", c => c.String());
            AlterColumn("dbo.Products", "Limage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Limage", c => c.Binary());
            AlterColumn("dbo.Products", "Simage", c => c.Binary());
        }
    }
}
