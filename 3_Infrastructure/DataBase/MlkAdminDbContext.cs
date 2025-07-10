using Microsoft.EntityFrameworkCore;
using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._3_Infrastructure.DataBase
{
    public class MlkAdminDbContext(DbContextOptions<MlkAdminDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<VoiceChannel> Voices { get; set; }
        public DbSet<TextChannel> Texts { get; set; }
    }
}
