import React, { useState } from 'react';

const ReminderForm = ({ onAddReminder }) => {
  const [name, setName] = useState('');
  const [date, setDate] = useState('');
  const [error, setError] = useState('');

  // Function to validate date format
  const validateDate = (dateString) => {
    const regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
    return regex.test(dateString);
  };

  // Function to convert date to ISO 8601 format
  const convertDateToISO = (dateString) => {
    const [day, month, year] = dateString.split('/');
    const date = new Date(`${year}-${month}-${day}`);
    return date.toISOString();
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (name && date) {
      if (validateDate(date)) {
        const formattedDate = convertDateToISO(date);
        onAddReminder({ name, date: formattedDate });
        setName('');
        setDate('');
        setError('');
      } else {
        setError('Data inválida. Use o formato dd/mm/yyyy.');
      }
    }
  };

  return (
    <div className="reminder-form">
      <h2>Novo lembrete</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nome</label>
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Nome do lembrete"
          />
        </div>
        <div>
          <label>Data</label>
          <input
            type="text"
            value={date}
            onChange={(e) => setDate(e.target.value)}
            placeholder="Data do lembrete (no formato dd/mm/yyyy)"
          />
          {error && <p style={{ color: 'red' }}>{error}</p>}
        </div>
        <button type="submit">Criar</button>
      </form>
    </div>
  );
};

export default ReminderForm;
