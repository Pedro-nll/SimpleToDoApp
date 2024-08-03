using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases.Errors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ReminderRepository : IReminderRepository
{
    private readonly ReminderDbContext _context;

    public ReminderRepository(ReminderDbContext context)
    {
        _context = context;
    }

    public ErrorOr<Created> CreateReminder(Reminder reminder)
    {
        _context.Reminders.Add(reminder);
        var result = _context.SaveChanges();
        return result > 0 ? Result.Created : RemindersErrors.Reminders.UnexpectedError;
    }

    public IEnumerable<Reminder> GetAllReminders()
    {
        return _context.Reminders.ToList();
    }

    public ErrorOr<Deleted> DeleteReminder(Guid id)
    {
        var reminder = _context.Reminders.Find(id);
        if (reminder == null)
        {
            return RemindersErrors.Reminders.NotFound;
        }

        _context.Reminders.Remove(reminder);
        var result = _context.SaveChanges();
        return result > 0 ? Result.Deleted : RemindersErrors.Reminders.UnexpectedError;
    }
}
