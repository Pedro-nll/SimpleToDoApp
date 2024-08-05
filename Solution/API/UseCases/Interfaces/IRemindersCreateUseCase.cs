using API.Entities;
using ErrorOr;
using TesteDTI.Contracts.Reminders;

namespace API.Usecases.Interfaces;

public interface IRemindersCreateUseCase
{
    ErrorOr<Reminder> CreateReminder(CreateReminderRequest reminder);
}