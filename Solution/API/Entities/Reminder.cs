using API.Usecases.Errors;
using ErrorOr;

namespace API.Entities;

public class Reminder
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime Date { get;  }

    private Reminder(Guid id, string name, DateTime date)
    {
        this.Date = date;
        this.Name = name;
        this.Id = id;
    }

    public static ErrorOr<Reminder> Create(string name, DateTime date)
    {
        List<Error> errors = new();
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(RemindersErrors.Reminders.InvalidName);
        }
        if (date <= DateTime.Now)
        {
            errors.Add(RemindersErrors.Reminders.InvalidDate);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Reminder(Guid.NewGuid(), name, date);
    }

}