namespace Project2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Project", "create_at");
            DropColumn("dbo.Project", "update_at");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project", "update_at", c => c.DateTime());
            AddColumn("dbo.Project", "create_at", c => c.DateTime());
        }
    }
}
