using Microsoft.EntityFrameworkCore;

namespace TelegramAccess.Entities
{
    public class PresentationContext : DbContext
    {
        public PresentationContext(DbContextOptions<PresentationContext> options) : base(options) { }

        public DbSet<Microsoft.AspNetCore.Identity.IdentityUserClaim<int>> IdentityUserClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Presentation> Presentations {get; set;}
        public DbSet<Question> Questions { get; set; }
    }
}
