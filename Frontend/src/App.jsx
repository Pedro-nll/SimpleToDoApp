import React, { useState, useEffect } from 'react';
import ReminderForm from './components/RemindersForm';
import ReminderList from './components/RemindersList';
import './App.css';
import { APIReq } from './APIReq';

function App() {
  const [reminders, setReminders] = useState([]);
  const [error, setError] = useState('');
  const [deletingReminderId, setDeletingReminderId] = useState(null);
  const [addingReminderId, setAddingReminderId] = useState(null); // New state for added reminder
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
        setAddingReminderId(response.data.id); // Set the ID of the added reminder
        setReminders((prevReminders) => [...prevReminders, response.data]);
        setError('');
        // Clear the ID after a short delay to allow animation
        setTimeout(() => setAddingReminderId(null), 500);
      }
    } catch (e) {
      console.error("Failed to add reminder:", e);
    }
  };

  const deleteReminder = async (id) => {
    try {
      setDeletingReminderId(id);
      await new Promise((resolve) => setTimeout(resolve, 1000)); // Wait for the red flash animation
      await REST.deleteRequest(`/reminders/${id}`);
      setReminders((prevReminders) => prevReminders.filter((reminder) => reminder.id !== id));
      setDeletingReminderId(null);
    } catch (e) {
      console.error("Failed to delete reminder:", e);
    }
  };

  return (
    <div className="app-container">
      <ReminderForm onAddReminder={addReminder} error={error} />
      <ReminderList
        reminders={reminders}
        onDeleteReminder={deleteReminder}
        deletingReminderId={deletingReminderId}
        addingReminderId={addingReminderId}
      />
    </div>
  );
}

export default App;
