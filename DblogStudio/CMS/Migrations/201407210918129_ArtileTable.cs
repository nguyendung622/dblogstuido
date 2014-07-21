namespace CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtileTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PublicDate = c.String(),
                        UserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subject", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.ExtraUserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "UserId", "dbo.ExtraUserProfile");
            DropForeignKey("dbo.Article", "SubjectId", "dbo.Subject");
            DropIndex("dbo.Article", new[] { "SubjectId" });
            DropIndex("dbo.Article", new[] { "UserId" });
            DropTable("dbo.Article");
        }
    }
}
