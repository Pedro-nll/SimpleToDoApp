using API.Entities;
using API.Usecases.Interfaces;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TesteDTI.Contracts.Reminders;

namespace API.Controllers;
public class RemindersController(IRemindersUsecases remindersUseCases) : ApiController
{
    private readonly IRemindersUsecases _remindersUseCases = remindersUseCases;
    
    [HttpPost]
    public IActionResult CreateReminder(CreateReminderRequest request)
    {
        ErrorOr<Reminder> createReminderResult = _remindersUseCases.CreateReminder(request);

        return createReminderResult.Match(
            reminder => CreatedAtAction(
                actionName: nameof(GetReminder),
                routeValues: new { id = reminder.Id },
                value: MapReminderResponse(reminder)
            ),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetReminder(Guid id)
    {
        ErrorOr<Reminder> reminderResult = _remindersUseCases.GetReminderById(id);

        return reminderResult.Match(
            reminder => Ok(MapReminderResponse(reminder)),
            errors => Problem(errors)
        );
    }

    [HttpGet()]
    public IActionResult GetAllReminders()
    {
        var reminders = _remindersUseCases.GetAllReminders();
        var response = reminders.Select(MapReminderResponse).ToList();
        
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteReminder(Guid id)
    {
        ErrorOr<Deleted> deletedResult = _remindersUseCases.DeleteReminder(id);

        return deletedResult.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
    
    private ReminderResponse MapReminderResponse(Reminder reminder)
    {
        return new ReminderResponse(
            reminder.Id,
            reminder.Name,
            reminder.Date
        );
    }

    private CreatedAtActionResult CreatedAsGetAllReminders(Reminder reminder)
    {
        return CreatedAtAction(
            actionName: nameof(GetAllReminders),
            value: MapReminderResponse(reminder)
        );
    }
}