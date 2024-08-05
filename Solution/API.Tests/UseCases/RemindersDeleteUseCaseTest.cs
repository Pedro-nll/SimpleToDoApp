using System;
using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases;
using API.Usecases.Errors;
using ErrorOr;
using Moq;
using Xunit;

namespace API.Tests.UseCases;

public class RemindersDeleteUseCaseTest
{
    [Fact]
    public void DeleteReminder_Success()
    {
        // Arrange
        var reminderRepo = new Mock<IReminderRepository>();
        var unit = new RemindersDeleteUseCase(reminderRepo.Object);
        var reminderId = Guid.NewGuid();
        var expectedDeleted = Result.Deleted;

        reminderRepo.Setup(repo => repo.DeleteReminder(reminderId))
                    .Returns(new Deleted());

        // Act
        var result = unit.DeleteReminder(reminderId);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(expectedDeleted, result.Value);
        reminderRepo.Verify(repo => repo.DeleteReminder(reminderId), Times.Once);
    }

    [Fact]
    public void DeleteReminder_NotFound()
    {
        // Arrange
        var reminderRepo = new Mock<IReminderRepository>();
        var unit = new RemindersDeleteUseCase(reminderRepo.Object);
        var reminderId = Guid.NewGuid();
        var expectedError = RemindersErrors.Reminders.NotFound;

        reminderRepo.Setup(repo => repo.DeleteReminder(reminderId))
                    .Returns(RemindersErrors.Reminders.NotFound);

        // Act
        var result = unit.DeleteReminder(reminderId);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(expectedError, result.Errors);
        reminderRepo.Verify(repo => repo.DeleteReminder(reminderId), Times.Once);
    }

    [Fact]
    public void DeleteReminder_UnexpectedError()
    {
        // Arrange
        var reminderRepo = new Mock<IReminderRepository>();
        var unit = new RemindersDeleteUseCase(reminderRepo.Object);
        var reminderId = Guid.NewGuid();
        var expectedError = RemindersErrors.Reminders.UnexpectedError;

        reminderRepo.Setup(repo => repo.DeleteReminder(reminderId))
                    .Returns(RemindersErrors.Reminders.UnexpectedError);

        // Act
        var result = unit.DeleteReminder(reminderId);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(expectedError, result.Errors);
        reminderRepo.Verify(repo => repo.DeleteReminder(reminderId), Times.Once);
    }
}
