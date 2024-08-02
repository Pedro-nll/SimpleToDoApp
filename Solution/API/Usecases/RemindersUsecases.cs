using API.Entities;
using API.Usecases.Errors;
using API.Usecases.Interfaces;
using ErrorOr;

namespace API.Usecases;

public class RemindersUsecases : IRemindersUsecases
{
    private static readonly Dictionary<Guid, Reminder> _reminders = new Dictionary<Guid, Reminder>();
    public ErrorOr<Created> CreateReminder(Reminder reminder)
    {
        _reminders.Add(reminder.Id, reminder);

        return Result.Created;
    }

    public IEnumerable<Reminder> GetAllReminders()
    {
        return _reminders.Values;
    }

    public ErrorOr<Deleted> DeleteReminder(Guid id)
    {
        if (_reminders.TryGetValue(id, out var reminder))
        {
            _reminders.Remove(id);
            return Result.Deleted;
        }

        return RemindersErrors.Reminders.NotFound;
    }
}