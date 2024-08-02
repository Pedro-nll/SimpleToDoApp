import React, { useState } from 'react';

const ReminderForm = ({ onAddReminder }) => {
  const [name, setName] = useState('');
  const [date, setDate] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (name && date) {
      onAddReminder({ name, date });
      setName('');
      setDate('');
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
        </div>
        <button type="submit">Criar</button>
      </form>
    </div>
  );
};

export default ReminderForm;