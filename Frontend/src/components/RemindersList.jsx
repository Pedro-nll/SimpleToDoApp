import React from 'react';

const ReminderList = ({ reminders, onDeleteReminder }) => {
  return (
    <div className="reminder-list">
      <h2>Lista de lembretes</h2>
      {reminders.map((reminder) => {
        const date = new Date(reminder.date);
        const formattedDate = date.toLocaleDateString('pt-BR');
        return (
          <div key={reminder.id} className="reminder">
            <div>{formattedDate}</div>
            <div>
              {reminder.name} 
              <button onClick={() => onDeleteReminder(reminder.id)}>❌</button>
            </div>
          </div>
        );
      })}
    </div>
  );
};

export default ReminderList;
