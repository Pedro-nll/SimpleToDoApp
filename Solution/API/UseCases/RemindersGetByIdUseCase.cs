using API.Entities;
using API.Repositories.Interfaces;
using API.Usecases.Interfaces;
using ErrorOr;

namespace API.Usecases;

public class RemindersGetByIdUseCase(IReminderRepository reminderRepository) : IRemindersGetByIdUseCase
{
    private readonly IReminderRepository _reminderRepository = reminderRepository;

    public ErrorOr<Reminder> GetReminderById(Guid id)
    {
        return _reminderRepository.GetReminderById(id);
    }
}