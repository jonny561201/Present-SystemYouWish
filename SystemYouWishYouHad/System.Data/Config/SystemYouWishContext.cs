using System.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace System.Data.Config;

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
