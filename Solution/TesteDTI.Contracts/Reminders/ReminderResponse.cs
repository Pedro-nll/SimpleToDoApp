

namespace TesteDTI.Contracts.Reminders;

public record ReminderResponse(
    Guid id,
    string name,
    DateTime date
);
