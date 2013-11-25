namespace Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserProfiles", "Email", c => c.String(nullable: false));
            AddColumn("UserProfiles", "IsBlocked", c => c.Boolean(nullable: false));

            CreateTable(
                "dbo.ImageDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        PublicId = c.String(),
                        Version = c.Int(nullable: false),
                        Signature = c.String(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Format = c.String(),
                        ResourceType = c.String(),
                        Bytes = c.Int(nullable: false),
                        Type = c.String(),
                        Url = c.String(),
                        SecureUrl = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ModifiedPhoto = c.String(),
                        ImageDataOriginalPhotoId = c.Int(nullable: false),
                        AmountOfLikes = c.Int(),
                        AmountOfDislikes = c.Int(),
                        CommonRaiting = c.Int(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageDatas", t => t.ImageDataOriginalPhotoId)
                .ForeignKey("dbo.UserProfiles", t => t.Owner_Id)
                .Index(t => t.ImageDataOriginalPhotoId)
                .Index(t => t.Owner_Id);
            
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserProfileId, t.PhotoId })
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Photos", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.UserProfileId)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.Dislikes",
                c => new
                    {
                        PhotoId = c.Int(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhotoId, t.UserProfileId })
                .ForeignKey("dbo.Photos", t => t.PhotoId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.PhotoId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.TagsPhotos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhotoId, t.TagId })
                .ForeignKey("dbo.Photos", t => t.PhotoId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PhotoId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagsPhotos", "TagId", "dbo.Tags");
            DropForeignKey("dbo.TagsPhotos", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.Dislikes", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Dislikes", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.Likes", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.Likes", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Photos", "Owner_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Photos", "ImageDataOriginalPhotoId", "dbo.ImageDatas");
            DropIndex("dbo.TagsPhotos", new[] { "TagId" });
            DropIndex("dbo.TagsPhotos", new[] { "PhotoId" });
            DropIndex("dbo.Dislikes", new[] { "UserProfileId" });
            DropIndex("dbo.Dislikes", new[] { "PhotoId" });
            DropIndex("dbo.Likes", new[] { "PhotoId" });
            DropIndex("dbo.Likes", new[] { "UserProfileId" });
            DropIndex("dbo.Photos", new[] { "Owner_Id" });
            DropIndex("dbo.Photos", new[] { "ImageDataOriginalPhotoId" });
            DropTable("dbo.TagsPhotos");
            DropTable("dbo.Dislikes");
            DropTable("dbo.Likes");
            DropTable("dbo.Tags");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Photos");
            DropTable("dbo.ImageDatas");
        }
    }
}
