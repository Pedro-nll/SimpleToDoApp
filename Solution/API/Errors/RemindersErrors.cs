using ErrorOr;

namespace API.Usecases.Errors;

public static class RemindersErrors
{
    public static class Reminders
    {
        public static Error InvalidDate => Error.Validation(
            "Reminder.InvalidDate",
            "Invalid date for the reminder."
        );
        public static Error InvalidName => Error.Validation(
            "Reminder.InvalidName",
            "Invalid name for the reminder."
        );
        public static Error NotFound => Error.NotFound(
            "Reminder.NotFound",
            "Reminder was not found."
        );
    }
}