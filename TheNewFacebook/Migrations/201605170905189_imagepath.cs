namespace TheNewFacebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagepath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsFeed", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsFeed", "ImagePath");
        }
    }
}
