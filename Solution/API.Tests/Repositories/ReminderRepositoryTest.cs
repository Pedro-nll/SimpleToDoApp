using System;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Usecases.Errors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class ReminderRepositoryTests
{
    private DbContextOptions<ReminderDbContext> GetInMemoryOptions()
    {
        return new DbContextOptionsBuilder<ReminderDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CreateReminder_ShouldReturnCreated_WhenSuccessful()
    {
        // Arrange
        var options = GetInMemoryOptions();
        using var context = new ReminderDbContext(options);
        var repository = new ReminderRepository(context);

        var reminder = Reminder.Create("Lembrete 1", DateTime.Now.AddDays(24)).Value;

        // Act
        var result = repository.CreateReminder(reminder);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(Result.Created, result.Value);
        Assert.Equal(1, context.Reminders.Count());
    }

    [Fact]
    public void GetAllReminders_ShouldReturnAllReminders()
    {
        // Arrange
        var options = GetInMemoryOptions();
        using var context = new ReminderDbContext(options);
        var repository = new ReminderRepository(context);

        var reminders = new List<Reminder>
        {
            Reminder.Create("Teste 1", DateTime.Now.AddDays(24)).Value,
            Reminder.Create("Teste 1", DateTime.Now.AddDays(24)).Value
        };
        context.Reminders.AddRange(reminders);
        context.SaveChanges();

        // Act
        var result = repository.GetAllReminders();

        // Assert
        Assert.Equal(reminders.Count, result.Count());
    }

    [Fact]
    public void DeleteReminder_ShouldReturnDeleted_WhenSuccessful()
    {
        // Arrange
        var options = GetInMemoryOptions();
        using var context = new ReminderDbContext(options);
        var repository = new ReminderRepository(context);

        var reminder = Reminder.Create("Teste 1", DateTime.Now.AddDays(24)).Value;
        context.Reminders.Add(reminder);
        context.SaveChanges();

        // Act
        var result = repository.DeleteReminder(reminder.Id);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(Result.Deleted, result.Value);
        Assert.Equal(0, context.Reminders.Count());
    }

    [Fact]
    public void DeleteReminder_ShouldReturnNotFound_WhenReminderDoesNotExist()
    {
        // Arrange
        var options = GetInMemoryOptions();
        using var context = new ReminderDbContext(options);
        var repository = new ReminderRepository(context);

        // Act
        var result = repository.DeleteReminder(Guid.NewGuid());

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(RemindersErrors.Reminders.NotFound, result.FirstError);
    }

    [Fact]
    public void GetReminderById_ShouldReturnReminder_WhenExists()
    {
        // Arrange
        var options = GetInMemoryOptions();
        using var context = new ReminderDbContext(options);
        var repository = new ReminderRepository(context);

        var reminder = Reminder.Create("Teste 1", DateTime.Now.AddDays(24)).Value;
        context.Reminders.Add(reminder);
        context.SaveChanges();

        // Act
        var result = repository.GetReminderById(reminder.Id);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(reminder.Id, result.Value.Id);
        Assert.Equal(reminder.Name, result.Value.Name);
        Assert.Equal(reminder.Date, result.Value.Date);
    }

    [Fact]
    public void GetReminderById_ShouldReturnNotFound_WhenReminderDoesNotExist()
    {
        // Arrange
        var options = GetInMemoryOptions();
        using var context = new ReminderDbContext(options);
        var repository = new ReminderRepository(context);

        // Act
        var result = repository.GetReminderById(Guid.NewGuid());

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(RemindersErrors.Reminders.NotFound, result.FirstError);
    }
}
