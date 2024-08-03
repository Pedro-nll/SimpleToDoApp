import React from 'react';

// Function to group reminders by date
const groupRemindersByDate = (reminders) => {
  if (!Array.isArray(reminders)) {
    throw new TypeError('Reminders is not an array');
  }

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

// Function to sort dates
const sortDates = (dates) => {
  if (!dates){
    return {}
  }
  return dates.sort((a, b) => {
    const dateA = a.split('/').reverse().join('');
    const dateB = b.split('/').reverse().join('');
    return dateA.localeCompare(dateB);
  });
};

const ReminderList = ({ reminders, onDeleteReminder, deletingReminderId, addingReminderId }) => {
  var sortedDates
  var groupedReminders
  try {
    groupedReminders = groupRemindersByDate(reminders);
    sortedDates = sortDates(Object.keys(groupedReminders));
  } catch (error) {
    console.log(error.message)
  }

  return (
    <div className="reminder-list">
      <h2>Lista de lembretes</h2>
      {sortedDates && sortedDates.map((date) => (
        <div key={date} className="reminder-group">
          <h3>{date}</h3>
          {groupedReminders && groupedReminders[date].map((reminder) => (
            <div
              key={reminder.id}
              className={`reminder ${deletingReminderId === reminder.id ? 'deleting' : ''} ${addingReminderId === reminder.id ? 'adding' : ''}`}
            >
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
