using System;
using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases;
using API.Usecases.Errors;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace API.Tests.UseCases;

[TestSubject(typeof(RemindersGetByIdUseCase))]
public class RemindersGetByIdUseCaseTest
{

    [Fact]
    public void GetReminderById_Success()
    {
        var remindersRepo = new Mock<IReminderRepository>();
        var unit = new RemindersGetByIdUseCase(remindersRepo.Object);
        var expectedReminder = Reminder.Create("Lembrete 1", DateTime.Now.AddHours(24)).Value;
        var reminderId = expectedReminder.Id;

        remindersRepo.Setup(r => r.GetReminderById(reminderId)).Returns(expectedReminder);

        var getReminderResult = unit.GetReminderById(reminderId);
        
        Assert.False(getReminderResult.IsError);
        Assert.Equal(expectedReminder, getReminderResult.Value);
    }
    
    [Fact]
    public void GetReminderById_NotFound()
    {
        var remindersRepo = new Mock<IReminderRepository>();
        var unit = new RemindersGetByIdUseCase(remindersRepo.Object);
        var expectedError = RemindersErrors.Reminders.NotFound;
        var reminderId = new Guid();

        remindersRepo.Setup(r => r.GetReminderById(reminderId)).Returns(expectedError);

        var getReminderResult = unit.GetReminderById(reminderId);
        
        Assert.True(getReminderResult.IsError);
        Assert.Equal(expectedError, getReminderResult.FirstError);
    }
}