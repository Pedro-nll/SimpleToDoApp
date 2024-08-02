using API.Entities;
using API.Usecases.Interfaces;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TesteDTI.Contracts.Reminders;

namespace API.Controllers;
public class RemindersController(IRemindersUsecases remindersUseCases) : ApiController
{
    private readonly IRemindersUsecases _remindersUseCases = remindersUseCases;
    
    [HttpPost()]
    public IActionResult CreateReminder(CreateReminderRequest request)
    {
        var requestToReminderResult = Reminder.Create(request.Name, request.Date);

        if (requestToReminderResult.IsError)
        {
            return Problem(requestToReminderResult.Errors);
        }

        var reminder = requestToReminderResult.Value;
        ErrorOr<Created> createReminderResult = _remindersUseCases.CreateReminder(reminder);

        return createReminderResult.Match(
            _ => CreatedAsGetAllReminders(reminder),
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