# Teste DTI API

- [Teste DTI API ](#teste-dti-api)
  - [Create Reminder](#create-reminder)
    - [Create Reminder Request](#create-reminder-request)  
    - [Create Reminder Response](#create-reminder-response)  
  - [Get Reminders](#get-reminders)
    - [Get Reminders Request](#get-reminders-request)  
    - [Get Reminders Response](#get-reminders-response)  
  - [Delete Reminder](#delete-reminder)
    - [Delete Reminder Request](#get-reminders-request)  
    - [Delete Reminder Response](#get-reminders-response)  

# Create Reminder

## Create Reminder Request
```
POST /reminders
```
```json
{
    "name": "Lembrete 1",
    "date": "31/12/2024"
}
```

## Create Reminder Response
```
201 created
```
```json
{
    "id": 1,
    "name": "Lembrete 1",
    "date": "31/12/2024"
}
```

or

```
400 Bad Request
```

# Get Reminders

## Get Reminders Request
```
GET /reminders
```

## Get Reminders Response
```json
[
    {
        "id": 1,
        "name": "Lembrete 1",
        "date": "31/12/2024"
    },
    {
        "id": 2,
        "name": "Lembrete 2",
        "date": "01/01/2025"
    }
]
```

# Delete Reminder

# Delete Reminder Request
```
DELETE /reminders/{{id}}
```

# Delete Reminder Response
```
204 No Content
```

or

```
404 No Content
```


