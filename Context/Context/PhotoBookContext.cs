using System.Data.Entity;
using DomainModels.Models;

namespace Context.Context
{
    public class PhotoBookContext : DbContext
    {
        public PhotoBookContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ImageData> ImageDatas { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }

        private static readonly PhotoBookContext Instance = new PhotoBookContext();

        public static PhotoBookContext Current
        {
            get { return Instance; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserProfile>()
                .HasMany(up => up.UserLikes)
                .WithMany(photo => photo.PhotoLikes)
                .Map(mc =>
                {
                    mc.ToTable("Likes");
                    mc.MapLeftKey("UserProfileId");
                    mc.MapRightKey("PhotoId");
                } 
            );

            modelBuilder.Entity<Photo>()
                .HasMany(up => up.PhotoDislikes)
                .WithMany(userProfile => userProfile.UserDislikes)
                .Map(mc =>
                {
                    mc.ToTable("Dislikes");
                    mc.MapLeftKey("PhotoId");
                    mc.MapRightKey("UserProfileId");
                }
            );

            modelBuilder.Entity<Photo>()
                .HasMany(up => up.Tags)
                .WithMany(tag => tag.Photos)
                .Map(mc =>
                {
                    mc.ToTable("TagsPhotos");
                    mc.MapLeftKey("PhotoId");
                    mc.MapRightKey("TagId");
                }
            );

            modelBuilder.Entity<Photo>()
                .HasRequired(im => im.OriginalPhoto)
                .WithOptional()
                .Map(mc => mc.MapKey("ImageDataOriginalPhotoId"));

            base.OnModelCreating(modelBuilder);
        }
    }
}