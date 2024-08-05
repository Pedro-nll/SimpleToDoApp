using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases.Interfaces;

namespace API.Usecases;

public class RemindersGetAllUseCase(IReminderRepository reminderRepository) : IRemindersGetAllUseCase
{
    private readonly IReminderRepository _reminderRepository = reminderRepository;
    
    public IEnumerable<Reminder> GetAllReminders()
    {
        return _reminderRepository.GetAllReminders();
    }
}