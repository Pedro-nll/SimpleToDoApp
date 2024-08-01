namespace TesteDTI.Contracts.Reminders;

public record CreateReminderRequest(
    string name, 
    DateTime date
);
