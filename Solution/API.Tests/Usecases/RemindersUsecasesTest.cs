using System;
using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases;
using ErrorOr;
using JetBrains.Annotations;
using Xunit;
using Moq;

namespace API.Tests.Usecases;

[TestSubject(typeof(RemindersUsecases))]
//TODO: REMINDERSCREATEUSECASE
public class RemindersUsecasesTest
{
    
    [Fact]
    public void TestCreteReminderUsecase()
    {
        var reminderRepository = new Mock<IReminderRepository>();
        var unit = new RemindersUsecases(reminderRepository.Object);
        
        var reminderResult = Reminder.Create("Lembrete1", DateTime.Now.Add(TimeSpan.FromHours(120)));
        Assert.False(reminderResult.IsError);
        var reminder = reminderResult.Value;
        var created = new Created();

        reminderRepository.Setup(r => r.CreateReminder(reminder)).Returns(created);
        Assert.Equal(created, unit.CreateReminder(reminder));
    }
}