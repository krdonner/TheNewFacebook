namespace TheNewFacebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Author = c.String(),
                        Newsfeed_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsFeed", t => t.Newsfeed_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Newsfeed_ID)
                .Index(t => t.User_ID);
            
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
                        Type = c.String(),
                        GroupName = c.String(),
                        Users_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Users_ID)
                .Index(t => t.Users_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        RelationshipStatus = c.String(nullable: false),
                        Workplace = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Information = c.String(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GroupUser",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.UserID })
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_ID", "dbo.Users");
            DropForeignKey("dbo.NewsFeed", "Users_ID", "dbo.Users");
            DropForeignKey("dbo.GroupUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.GroupUser", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Comments", "Newsfeed_ID", "dbo.NewsFeed");
            DropIndex("dbo.GroupUser", new[] { "UserID" });
            DropIndex("dbo.GroupUser", new[] { "GroupID" });
            DropIndex("dbo.NewsFeed", new[] { "Users_ID" });
            DropIndex("dbo.Comments", new[] { "User_ID" });
            DropIndex("dbo.Comments", new[] { "Newsfeed_ID" });
            DropTable("dbo.GroupUser");
            DropTable("dbo.Groups");
            DropTable("dbo.Users");
            DropTable("dbo.NewsFeed");
            DropTable("dbo.Comments");
        }
    }
}
