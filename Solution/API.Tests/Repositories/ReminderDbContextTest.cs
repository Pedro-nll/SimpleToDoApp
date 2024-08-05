using System;
using API.Entities;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ReminderDbContextTests
{
    private DbContextOptions<ReminderDbContext> GetInMemoryOptions()
    {
        return new DbContextOptionsBuilder<ReminderDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CanAddReminder()
    {
        // Arrange
        var options = GetInMemoryOptions();
        var savedReminder = Reminder.Create("Test Reminder", DateTime.Now.AddHours(24)).Value;

        using (var context = new ReminderDbContext(options))
        {
            // Act
            context.Reminders.Add(savedReminder);
            context.SaveChanges();
        }

        // Assert
        using (var context = new ReminderDbContext(options))
        {
            Assert.Equal(1, context.Reminders.CountAsync().Result);
            var reminder = context.Reminders.FirstAsync().Result;
            Assert.Equal("Test Reminder", reminder.Name);
        }
    }
}