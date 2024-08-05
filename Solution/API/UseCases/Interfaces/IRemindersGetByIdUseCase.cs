using API.Entities;
using ErrorOr;

namespace API.Usecases.Interfaces;

public interface IRemindersGetByIdUseCase
{
    ErrorOr<Reminder> GetReminderById(Guid id);
}