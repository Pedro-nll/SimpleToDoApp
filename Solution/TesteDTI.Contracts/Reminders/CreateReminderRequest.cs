namespace TesteDTI.Contracts.Reminders;

public record CreateReminderRequest(
    string Name, 
    DateTime Date
);
