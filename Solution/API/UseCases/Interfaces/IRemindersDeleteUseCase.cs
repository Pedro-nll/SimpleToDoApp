using ErrorOr;

namespace API.Usecases.Interfaces;

public interface IRemindersDeleteUseCase
{
    ErrorOr<Deleted> DeleteReminder(Guid id);
}