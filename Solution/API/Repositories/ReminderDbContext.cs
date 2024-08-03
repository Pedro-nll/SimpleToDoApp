using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Repositories;

public class ReminderDbContext : DbContext
{
    public ReminderDbContext(DbContextOptions<ReminderDbContext> options) : base(options)
    {
    }

    public DbSet<Reminder> Reminders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reminder>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Reminder>()
            .Property(r => r.Name)
            .IsRequired();

        modelBuilder.Entity<Reminder>()
            .Property(r => r.Date)
            .IsRequired();
    }
}
