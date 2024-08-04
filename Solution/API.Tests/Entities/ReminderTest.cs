using System;
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
        
    }
    
    [Fact]
    public void TestCreateReminderInvalidNameEmpty()
    {
        //empty string
    }
    
    [Fact]
    public void TestCreateReminderInvalidNameTooBig()
    {
        //301 chars
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