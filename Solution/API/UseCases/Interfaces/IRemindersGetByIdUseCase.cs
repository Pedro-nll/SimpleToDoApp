using API.Entities;
using ErrorOr;

namespace API.Usecases.Interfaces;

public interface IReminderGetByIdUseCase
{
    ErrorOr<Reminder> GetReminderById(Guid id);
}