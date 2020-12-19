namespace Project2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Full_name = c.String(nullable: false),
                        Path = c.String(),
                        Birthday = c.DateTime(),
                        Email = c.String(),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permission", t => t.PermissionId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        create_at = c.DateTime(nullable: false),
                        update_at = c.DateTime(),
                        isActive = c.Boolean(),
                        require = c.String(nullable: false),
                        TimeStartId = c.Int(),
                        StudentId = c.Int(),
                        TeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Time_start", t => t.TimeStartId, cascadeDelete: true)
                .ForeignKey("dbo.Guest", t => t.StudentId)
                .ForeignKey("dbo.Guest", t => t.TeacherId)
                .Index(t => t.TimeStartId)
                .Index(t => t.StudentId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        content = c.String(nullable: false),
                        files = c.String(),
                        create_at = c.DateTime(nullable: false),
                        ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Time_start",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        start_at = c.DateTime(nullable: false),
                        end_at = c.DateTime(),
                        numberProject = c.Int(nullable: false),
                        numberProjectInTeacher = c.Int(nullable: false),
                        isFinish = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProjectTag",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.TagId })
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "TeacherId", "dbo.Guest");
            DropForeignKey("dbo.Project", "StudentId", "dbo.Guest");
            DropForeignKey("dbo.Project", "TimeStartId", "dbo.Time_start");
            DropForeignKey("dbo.ProjectTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.ProjectTag", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Report", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Guest", "PermissionId", "dbo.Permission");
            DropIndex("dbo.ProjectTag", new[] { "TagId" });
            DropIndex("dbo.ProjectTag", new[] { "ProjectId" });
            DropIndex("dbo.Report", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "TeacherId" });
            DropIndex("dbo.Project", new[] { "StudentId" });
            DropIndex("dbo.Project", new[] { "TimeStartId" });
            DropIndex("dbo.Guest", new[] { "PermissionId" });
            DropTable("dbo.ProjectTag");
            DropTable("dbo.Time_start");
            DropTable("dbo.Tag");
            DropTable("dbo.Report");
            DropTable("dbo.Project");
            DropTable("dbo.Permission");
            DropTable("dbo.Guest");
        }
    }
}
