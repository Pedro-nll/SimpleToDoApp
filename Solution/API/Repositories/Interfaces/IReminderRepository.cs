using API.Entities;
using ErrorOr;

namespace API.Repositories.Interfaces;

public interface IReminderRepository
{
    ErrorOr<Created> CreateReminder(Reminder reminder);
    IEnumerable<Reminder> GetAllReminders();
    ErrorOr<Deleted> DeleteReminder(Guid id);
    ErrorOr<Reminder> GetReminderById(Guid id);
}