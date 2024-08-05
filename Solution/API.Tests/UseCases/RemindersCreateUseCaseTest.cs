using System;
using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases;
using API.Usecases.Errors;
using ErrorOr;
using Moq;
using TesteDTI.Contracts.Reminders;
using Xunit;

namespace API.Tests.UseCases;

public class RemindersCreateUseCaseTest
{
    [Fact]
    public void CreateReminder_Success()
    {
        // Dependency
        var reminderRepo = new Mock<IReminderRepository>();
        // DI on class
        var unit = new RemindersCreateUseCase(reminderRepo.Object);
        // Test arg
        var reminderArg = new CreateReminderRequest("Lembrete 1", DateTime.Now.AddDays(2));
        // Expected output
        var expectedReminder = Reminder.Create(reminderArg.Name, reminderArg.Date).Value;
        
        reminderRepo.Setup(repo => repo.CreateReminder(It.IsAny<Reminder>()))
            .Returns(new Created());
        
        var result = unit.CreateReminder(reminderArg);
        
        Assert.False(result.IsError);
        Assert.Equal(expectedReminder.Date, result.Value.Date);
        Assert.Equal(expectedReminder.Name, result.Value.Name);
        reminderRepo.Verify(repo => repo.CreateReminder(It.IsAny<Reminder>()), Times.Once);
    }

    [Fact]
    public void CreateReminder_Failure()
    {
        var reminderRepo = new Mock<IReminderRepository>();
        var unit = new RemindersCreateUseCase(reminderRepo.Object);
        var reminderArg = new CreateReminderRequest("Lembrete 1", DateTime.Now.AddDays(-2));
        var expectedError = RemindersErrors.Reminders.InvalidDate;

        var result = unit.CreateReminder(reminderArg);

        Assert.True(result.IsError);
        Assert.Equal(expectedError.Code, result.FirstError.Code);
    }
}