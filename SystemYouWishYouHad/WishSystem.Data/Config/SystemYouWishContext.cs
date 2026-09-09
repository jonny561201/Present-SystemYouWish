using Microsoft.EntityFrameworkCore;
using WishSystem.Data.Models;

namespace WishSystem.Data.Config;

public partial class SystemYouWishContext : DbContext
{
    public SystemYouWishContext() {}

    public SystemYouWishContext(DbContextOptions<SystemYouWishContext> options) : base(options) {}


    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}
