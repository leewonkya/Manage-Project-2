namespace Project2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Report", "isSeen", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Report", "isSeen");
        }
    }
}
