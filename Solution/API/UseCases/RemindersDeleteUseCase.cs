using API.Repositories.Interfaces;
using API.Usecases.Interfaces;
using ErrorOr;

namespace API.Usecases;

public class RemindersDeleteUseCase(IReminderRepository reminderRepository) : IRemindersDeleteUseCase
{
    private readonly IReminderRepository _reminderRepository = reminderRepository;

    public ErrorOr<Deleted> DeleteReminder(Guid id)
    {
        return _reminderRepository.DeleteReminder(id);
    }
}