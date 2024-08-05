using System;
using System.Text;
using API.Entities;
using API.Usecases.Errors;
using JetBrains.Annotations;
using Xunit;

namespace API.Tests.Entities;

[TestSubject(typeof(Reminder))]
public class ReminderTest
{

    [Fact]
    public void TestCreateReminderSuccess()
    {
        var date = DateTime.Now.AddHours(24);
        var reminderResult = Reminder.Create("Lembrete 1", date);
        
        Assert.False(reminderResult.IsError);
        Assert.Equal("Lembrete 1", reminderResult.Value.Name);
        Assert.Equal(date, reminderResult.Value.Date);
        Assert.NotEqual(Guid.Empty, reminderResult.Value.Id);
    }
    
    [Fact]
    public void TestCreateReminderInvalidNameEmpty()
    {
        var reminderResult = Reminder.Create("", DateTime.Now);
        
        Assert.Equal("Reminder.InvalidName", reminderResult.FirstError.Code);
    }
    
    [Fact]
    public void TestCreateReminderInvalidNameTooBig()
    {
        var reminderResult = Reminder.Create("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB", DateTime.Now);
        
        Assert.Equal("Reminder.InvalidName", reminderResult.FirstError.Code);
    }
    
    [Fact]
    public void TestCreateReminderInvalidDateToday()
    {
        var reminderResult = Reminder.Create("Lembrete1", DateTime.Now);
        
        Assert.Equal("Reminder.InvalidDate", reminderResult.FirstError.Code);
    }
    
    [Fact]
    public void TestCreateReminderInvalidDateYesterday()
    {
        var reminderResult = Reminder.Create("Lembrete1", DateTime.Now.AddDays(-1));
        
        Assert.Equal("Reminder.InvalidDate", reminderResult.FirstError.Code);
    }
    
}