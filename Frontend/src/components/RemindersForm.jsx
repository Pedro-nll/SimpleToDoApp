import React, { useEffect, useState } from 'react';

const ReminderForm = ({ onAddReminder, errorArg }) => {
  const [name, setName] = useState('');
  const [date, setDate] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    setError(errorArg)
  }, [errorArg])

  const validateDate = (dateString) => {
    const regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
    return regex.test(dateString);
  };

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
        setError('')
      } else {
        setError('Data inválida. Use o formato dd/mm/yyyy.');
      }
    } else {
      setError('Preencha todos os campos.');
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
