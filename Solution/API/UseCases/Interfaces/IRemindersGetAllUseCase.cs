using API.Entities;

namespace API.Usecases.Interfaces;

public interface IRemindersGetAllUseCase
{
    IEnumerable<Reminder> GetAllReminders();
}