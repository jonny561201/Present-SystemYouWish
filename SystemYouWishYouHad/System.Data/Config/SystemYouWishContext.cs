using System.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace System.Data.Config;

public partial class SystemYouWishConfig : DbContext
{
    public SystemYouWishConfig() {}

    public SystemYouWishConfig(DbContextOptions<SystemYouWishConfig> options) : base(options) {}


    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}
