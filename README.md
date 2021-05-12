# WebService.REST
REST-сервис для “платежной” системы. Данные работы сервиса храниться в SQL LocalDB.

В системе следующие методы:
- Balance(userId) - возвращает текущий остаток средств на счете пользователя с идентификатором userId
- History(userId, fromDate, toDate) - возвращает историю транзакций пользователя с идентификатором userId за период между from и to
- AddTransaction(userId, transactionTime, amount, notes) - добавляет транзакцию пользователю userId на дату/время transactionTime с комментарием notes). amount может быть как положительным так и отрицательным. При добавлении любой транзакции должно выполняться условие, что баланс пользователя не может стать отрицательным.
- Statistic(onDate) - выводит статистику за день по работе сервиса в разрезе пользователя, для всех пользователей системы сумма приходов за день и сумма расходов за день (список {userId, In, Out}).
