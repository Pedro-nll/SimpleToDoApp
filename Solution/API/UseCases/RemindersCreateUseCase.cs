using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases.Interfaces;
using ErrorOr;
using TesteDTI.Contracts.Reminders;

namespace API.Usecases;

public class RemindersCreateUseCase(IReminderRepository reminderRepository) : IRemindersCreateUseCase
{
    private readonly IReminderRepository _reminderRepository = reminderRepository;

    public ErrorOr<Reminder> CreateReminder(CreateReminderRequest request)
    {
        var requestToReminderResult = Reminder.Create(request.Name, request.Date);
        
        if (requestToReminderResult.IsError)
        {
            return requestToReminderResult.Errors;
        }

        var reminder = requestToReminderResult.Value;
        var createReminderResult = _reminderRepository.CreateReminder(reminder);
        
        return createReminderResult.IsError ? createReminderResult.Errors : reminder;
    }
}