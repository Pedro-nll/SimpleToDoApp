namespace TesteDTI.Contracts.Reminders;

public record ReminderResponse(
    Guid Id,
    string Name,
    DateTime Date
);
