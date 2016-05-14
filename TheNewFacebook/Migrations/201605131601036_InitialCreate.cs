namespace TheNewFacebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Image = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        RelationshipStatus = c.String(),
                        Workplace = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropForeignKey("dbo.GroupUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.GroupUser", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.NewsFeed", "Users_ID", "dbo.Users");
            DropIndex("dbo.GroupUser", new[] { "UserID" });
            DropIndex("dbo.GroupUser", new[] { "GroupID" });
            DropIndex("dbo.NewsFeed", new[] { "Users_ID" });
            DropTable("dbo.GroupUser");
            DropTable("dbo.NewsFeed");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
        }
    }
}
