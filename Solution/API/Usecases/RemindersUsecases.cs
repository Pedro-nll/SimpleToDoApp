using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases.Errors;
using API.Usecases.Interfaces;
using ErrorOr;

namespace API.Usecases;

public class RemindersUsecases(IReminderRepository reminderRepository) : IRemindersUsecases
{
    private readonly IReminderRepository _reminderRepository = reminderRepository;

    public ErrorOr<Created> CreateReminder(Reminder reminder)
    {
        return _reminderRepository.CreateReminder(reminder);
    }

    public IEnumerable<Reminder> GetAllReminders()
    {
        return _reminderRepository.GetAllReminders();
    }

    public ErrorOr<Deleted> DeleteReminder(Guid id)
    {
        return _reminderRepository.DeleteReminder(id);
    }
}