namespace CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtraUserProfile", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExtraUserProfile", "Department");
        }
    }
}
