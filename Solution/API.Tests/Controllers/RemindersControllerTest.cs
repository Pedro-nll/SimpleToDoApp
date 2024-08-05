using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Entities;
using API.Usecases.Interfaces;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TesteDTI.Contracts.Reminders;

namespace API.Tests.Controllers
{
    public class RemindersControllerTests
    {
        private readonly Mock<IRemindersCreateUseCase> _mockCreateUseCase;
        private readonly Mock<IRemindersDeleteUseCase> _mockDeleteUseCase;
        private readonly Mock<IRemindersGetAllUseCase> _mockGetAllUseCase;
        private readonly Mock<IRemindersGetByIdUseCase> _mockGetByIdUseCase;
        private readonly RemindersController _controller;

        public RemindersControllerTests()
        {
            _mockCreateUseCase = new Mock<IRemindersCreateUseCase>();
            _mockDeleteUseCase = new Mock<IRemindersDeleteUseCase>();
            _mockGetAllUseCase = new Mock<IRemindersGetAllUseCase>();
            _mockGetByIdUseCase = new Mock<IRemindersGetByIdUseCase>();
            _controller = new RemindersController(
                _mockCreateUseCase.Object,
                _mockDeleteUseCase.Object,
                _mockGetAllUseCase.Object,
                _mockGetByIdUseCase.Object
            );
        }

        [Fact]
        public void CreateReminder_ShouldReturnCreatedAtAction_WhenReminderIsCreatedSuccessfully()
        {
            // Arrange
            var request = new CreateReminderRequest("Teste 1", DateTime.Today.AddDays(1));
            var reminder = Reminder.Create("Teste 1", DateTime.Today.AddDays(1)).Value;
            _mockCreateUseCase.Setup(x => x.CreateReminder(request)).Returns(reminder);

            // Act
            var result = _controller.CreateReminder(request) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("GetReminder", result.ActionName);
            Assert.Equal(reminder.Id, result.RouteValues["id"]);
            var response = Assert.IsType<ReminderResponse>(result.Value);
            Assert.Equal(reminder.Id, response.Id);
            Assert.Equal(reminder.Name, response.Name);
            Assert.Equal(reminder.Date, response.Date);
        }

        [Fact]
        public void GetReminder_ShouldReturnOk_WhenReminderIsFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new CreateReminderRequest("Teste 1", DateTime.Today.AddDays(1));
            var reminder = Reminder.Create("Teste 1", DateTime.Today.AddDays(1)).Value;
            _mockGetByIdUseCase.Setup(x => x.GetReminderById(id)).Returns(reminder);

            // Act
            var result = _controller.GetReminder(id) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var response = Assert.IsType<ReminderResponse>(result.Value);
            Assert.Equal(reminder.Id, response.Id);
            Assert.Equal(reminder.Name, response.Name);
            Assert.Equal(reminder.Date, response.Date);
        }

        [Fact]
        public void GetAllReminders_ShouldReturnOkWithReminders_WhenRemindersAreFound()
        {
            // Arrange
            var reminders = new List<Reminder>
            {
                Reminder.Create("Teste 1", DateTime.Today.AddDays(1)).Value,
                Reminder.Create("Teste 1", DateTime.Today.AddDays(1)).Value
            };
            _mockGetAllUseCase.Setup(x => x.GetAllReminders()).Returns(reminders);

            // Act
            var result = _controller.GetAllReminders() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var response = Assert.IsType<List<ReminderResponse>>(result.Value);
            Assert.Equal(reminders.Count, response.Count);
        }

        [Fact]
        public void DeleteReminder_ShouldReturnNoContent_WhenReminderIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockDeleteUseCase.Setup(x => x.DeleteReminder(id)).Returns(new Deleted());

            // Act
            var result = _controller.DeleteReminder(id) as NoContentResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
