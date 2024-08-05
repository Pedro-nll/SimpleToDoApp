using System;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases;
using API.Usecases.Errors;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace API.Tests.UseCases;

[TestSubject(typeof(RemindersGetAllUseCase))]
public class RemindersGetAllUseCaseTest
{

    [Fact]
    public void GetAllReminders_EmptyList()
    {
        var remindersRepo = new Mock<IReminderRepository>();
        var unit = new RemindersGetAllUseCase(remindersRepo.Object);

        remindersRepo.Setup(r => r.GetAllReminders()).Returns(Enumerable.Empty<Reminder>());

        var getAllResult = unit.GetAllReminders();
        
        Assert.False(getAllResult.Any());
    }
    
    [Fact]
    public void GetAllReminders_ShouldReturnAllReminders()
    {
        // Arrange
        var remindersRepo = new Mock<IReminderRepository>();
        var remindersUseCase = new RemindersGetAllUseCase(remindersRepo.Object);

        var reminders = new List<Reminder>
        {
            Reminder.Create("Lembrete1", DateTime.Now.AddHours(24)).Value,
            Reminder.Create("Lembrete2", DateTime.Now.AddHours(24)).Value,
            Reminder.Create("Lembrete3", DateTime.Now.AddHours(24)).Value
        };

        remindersRepo.Setup(r => r.GetAllReminders()).Returns(reminders);

        // Act
        var result = remindersUseCase.GetAllReminders();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(reminders.Count, result.Count());
        foreach (var reminder in reminders)
        {
            Assert.Contains(reminder, result);
        }
    }

}