using API.Entities;
using ErrorOr;
using TesteDTI.Contracts.Reminders;

namespace API.Usecases.Interfaces;

public interface IRemindersUsecases
{
    ErrorOr<Reminder> CreateReminder(CreateReminderRequest reminder);
    IEnumerable<Reminder> GetAllReminders();
    ErrorOr<Deleted> DeleteReminder(Guid id);

    ErrorOr<Reminder> GetReminderById(Guid id);
}