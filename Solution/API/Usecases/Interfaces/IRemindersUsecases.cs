using API.Entities;
using ErrorOr;

namespace API.Usecases.Interfaces;

public interface IRemindersUsecases
{
    ErrorOr<Created> CreateReminder(Reminder reminder);
    IEnumerable<Reminder> GetAllReminders();
    ErrorOr<Deleted> DeleteReminder(Guid id);
}