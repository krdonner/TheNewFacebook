namespace TheNewFacebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsFeed",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        updateDate = c.DateTime(nullable: false),
                        likes = c.Int(nullable: false),
                        Author = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsFeed");
        }
    }
}
