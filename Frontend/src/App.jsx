import React, { useState, useEffect } from 'react';
import ReminderForm from './components/RemindersForm';
import ReminderList from './components/RemindersList';
import './App.css';
import { APIReq } from './APIReq';

function App() {
  const [reminders, setReminders] = useState([]);
  const [error, setError] = useState('');
  const REST = new APIReq("http://localhost:5107");

  useEffect(() => {
    fetchReminders();
  }, []);

  const fetchReminders = async () => {
    try {
      const response = await REST.getRequest("/reminders");
      setReminders(response);
    } catch (e) {
      console.error("Failed to fetch reminders:", e);
    }
  };

  const addReminder = async (reminder) => {
    try {
      const response = await REST.postRequest("/reminders", JSON.stringify(reminder));
      if (response.status == 400){
        setError('Falha ao adicionar lembrete. Verifique se a data é futura e o nome válido.');
      }
      else{
        setReminders((prevReminders) => [...prevReminders, response.data]);
        setError('');
      }
    } catch (e) {
      console.error("Failed to add reminder:", e);
    }
  };

  const deleteReminder = async (id) => {
    try {
      await REST.deleteRequest(`/reminders/${id}`);
      setReminders((prevReminders) => prevReminders.filter((reminder) => reminder.id !== id));
    } catch (e) {
      console.error("Failed to delete reminder:", e);
    }
  };

  return (
    <div className="app-container">
      <ReminderForm onAddReminder={addReminder} error={error} />
      <ReminderList reminders={reminders} onDeleteReminder={deleteReminder} />
    </div>
  );
}

export default App;
