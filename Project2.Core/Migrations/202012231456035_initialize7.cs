namespace Project2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Report", "files");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Report", "files", c => c.String());
        }
    }
}
