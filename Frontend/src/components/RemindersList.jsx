import React from 'react';

const groupRemindersByDate = (reminders) => {
  return reminders.reduce((groups, reminder) => {
    const dateParts = reminder.date.split('T')[0].split('-');
    const date = new Date(dateParts[0], dateParts[1] - 1, dateParts[2]);
    const formattedDate = date.toLocaleDateString('pt-BR');

    if (!groups[formattedDate]) {
      groups[formattedDate] = [];
    }
    groups[formattedDate].push(reminder);

    return groups;
  }, {});
};

const sortDates = (dates) => {
  return dates.sort((a, b) => {
    const dateA = a.split('/').reverse().join('');
    const dateB = b.split('/').reverse().join('');
    return dateA.localeCompare(dateB);
  });
};

const ReminderList = ({ reminders, onDeleteReminder }) => {
  const groupedReminders = groupRemindersByDate(reminders);
  const sortedDates = sortDates(Object.keys(groupedReminders));

  return (
    <div className="reminder-list">
      <h2>Lista de lembretes</h2>
      {sortedDates.map((date) => (
        <div key={date} className="reminder-group">
          <h3>{date}</h3>
          {groupedReminders[date].map((reminder) => (
            <div key={reminder.id} className="reminder">
              <div>
                {reminder.name}
                <button onClick={() => onDeleteReminder(reminder.id)}>❌</button>
              </div>
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};

export default ReminderList;
