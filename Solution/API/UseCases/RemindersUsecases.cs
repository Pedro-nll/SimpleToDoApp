using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases.Errors;
using API.Usecases.Interfaces;
using ErrorOr;
using TesteDTI.Contracts.Reminders;

namespace API.Usecases;

public class RemindersUsecases(IReminderRepository reminderRepository) : IRem
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

    public IEnumerable<Reminder> GetAllReminders()
    {
        return _reminderRepository.GetAllReminders();
    }
    
    public ErrorOr<Reminder> GetReminderById(Guid id)
    {
        return _reminderRepository.GetReminderById(id);
    }

    public ErrorOr<Deleted> DeleteReminder(Guid id)
    {
        return _reminderRepository.DeleteReminder(id);
    }
}