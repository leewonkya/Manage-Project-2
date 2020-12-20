namespace Project2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "name", c => c.String());
            AlterColumn("dbo.Project", "create_at", c => c.DateTime());
            AlterColumn("dbo.Project", "require", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "require", c => c.String(nullable: false));
            AlterColumn("dbo.Project", "create_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Project", "name", c => c.String(nullable: false));
        }
    }
}
