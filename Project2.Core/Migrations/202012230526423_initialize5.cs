namespace Project2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Report", "title", c => c.String());
            AlterColumn("dbo.Report", "content", c => c.String());
            AlterColumn("dbo.Report", "create_at", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Report", "create_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Report", "content", c => c.String(nullable: false));
            AlterColumn("dbo.Report", "title", c => c.String(nullable: false));
        }
    }
}
