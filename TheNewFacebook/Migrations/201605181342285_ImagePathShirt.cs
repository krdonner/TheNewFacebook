namespace TheNewFacebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagePathShirt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shirts", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shirts", "ImagePath");
        }
    }
}
