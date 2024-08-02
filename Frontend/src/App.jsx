import { useState, useEffect } from 'react';
import ReminderForm from './components/RemindersForm';
import ReminderList from './components/RemindersList';
import './App.css';
import { APIReq } from './APIReq';

function App() {
  const [reminders, setReminders] = useState([]);
  const REST = new APIReq("http://localhost:5107")

  useEffect(() => {
    fetchReminders();
  }, []);

  const fetchReminders = async () => {
    try{
      const response = await REST.getRequest("/reminders")
      const data = await response.json();
      setReminders(data);
    }catch (e){
      console.error(e)
    }
  };

  const addReminder = async (reminder) => {
    const response = await REST.postRequest("/reminders", JSON.stringify(reminder))
    
    const newReminder = await response.json();
    setReminders((prevReminders) => [...prevReminders, newReminder]);
  };

  const deleteReminder = async (id) => {
    REST.deleteRequest(`/reminders/${id}`)
   
    setReminders((prevReminders) => prevReminders.filter((reminder) => reminder.id !== id));
  };

  return (
    <div className="app-container">
      <ReminderForm onAddReminder={addReminder} />
      <ReminderList reminders={reminders} onDeleteReminder={deleteReminder} />
    </div>
  );
}

export default App;
