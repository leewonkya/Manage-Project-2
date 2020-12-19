namespace Project2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Time_start", "name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Time_start", "name");
        }
    }
}
