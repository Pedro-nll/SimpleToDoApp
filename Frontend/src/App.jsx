import React, { useState, useEffect } from 'react';
import ReminderForm from './components/RemindersForm';
import ReminderList from './components/RemindersList';
import './App.css';
import { APIReq } from './APIReq';

function App() {
  const [reminders, setReminders] = useState([]);
  const [error, setError] = useState('');
  const [deletingReminderId, setDeletingReminderId] = useState(null);
  const [addingReminderId, setAddingReminderId] = useState(null);
  const REST = new APIReq("http://localhost:5107");

  useEffect(() => {
    fetchReminders();
  }, []);

  const fetchReminders = async () => {
    try {
      const response = await REST.getRequest("/reminders");
      if (!response) {
        setError("Não foi possível buscar os lembretes. Verifique se a API está disponível.")
        return;
      }
      setReminders(response);
      setError('');
    } catch (e) {
      console.error("Failed to fetch reminders:", e);
      setError("Não foi possível buscar os lembretes.");
    }
  };

  const addReminder = async (reminder) => {
    setError('')
    try {
      const response = await REST.postRequest("/reminders", JSON.stringify(reminder));
      if (response.status === 400) {
        setError('Falha ao adicionar lembrete. Verifique se a data é futura e o nome válido.');
      } else {
        setError('');
        setAddingReminderId(response.data.id); 
        setReminders((prevReminders) => [...prevReminders, response.data]);
        setError('');
        setTimeout(() => setAddingReminderId(null), 300);
      }
    } catch (e) {
      console.error("Failed to add reminder:", e);
      setError("Falha ao adicionar lembrete.");
    }
  };

  const deleteReminder = async (id) => {
    try {
      setDeletingReminderId(id);
      await new Promise((resolve) => setTimeout(resolve, 300));
      await REST.deleteRequest(`/reminders/${id}`);
      setReminders((prevReminders) => prevReminders.filter((reminder) => reminder.id !== id));
      setDeletingReminderId(null);
    } catch (e) {
      console.error("Failed to delete reminder:", e);
      setError("Falha ao deletar lembrete.");
    }
  };

  return (
    <div className="app-container">
      <ReminderForm onAddReminder={addReminder} errorArg={error} />
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
