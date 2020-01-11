using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramAccess.Entities
{
    public class PresentationContext : DbContext
    {
        public PresentationContext(DbContextOptions<PresentationContext> options) : base(options) { }

        public DbSet<Microsoft.AspNetCore.Identity.IdentityUserClaim<int>> IdentityUserClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Presentation> Presentations {get; set;}
    }
}
