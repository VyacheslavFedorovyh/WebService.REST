using System;
using System.Collections.Generic;
using System.Transactions;
using WebApi.ViewModels;

namespace WebApi.Interfaces
{
	public interface ITransactionRepository
	{
        /// <summary>
        /// Метод получения всех транзанзакций
        /// </summary>
        /// <returns></returns>
        List<TransactionView> GetAll();

		/// <summary>
		/// Метод получения остатка счета пользователя
		/// </summary>
		/// <param name="userId">Ид пользователя</param>
		/// <returns></returns>
		int? GetBalanceByUser(long? userId);

		/// <summary>
		/// Возвращает историю транзакций пользователя с идентификатором userID за указанный период
		/// </summary>
		/// <param name="userID">Ид пользователя</param>
		/// <param name="fromDate">Дата с</param>
		/// <param name="toDate">Дата по</param>
		/// <returns></returns>
		List<TransactionView> HistoryTransaction(long? userID, DateTime? fromDate, DateTime? toDate);

		/// <summary>
		/// Выводит статистику за день по работе сервиса в разрезе пользователя
		/// </summary>
		/// <param name="userID">Ид пользователя</param>
		/// <param name="date">Дата</param>
		/// <returns></returns>
		List<TransactionView> StatisticTransaction(long? userID, DateTime? date);

		/// <summary>
		/// Выводит статистику за день по работе сервиса в разрезе для всех пользователей системы сумма приходов или расходов
		/// </summary>
		/// <param name="Date">Дата</param>
		/// <param name="operation">Приход=true Расход=false</param>
		/// <returns></returns>
		List<TransactionView> StatisticTransaction(DateTime? Date, bool operation);

		/// <summary>
		/// Добавление транзакция
		/// </summary>
		/// <param name="userID">Ид пользователя</param>
		/// <param name="notes">Комментарий</param>
		/// <param name="amount">Сумма</param>
		/// <returns></returns>
		string AddTransaction(long? userID, int amount, string notes);
	}
}
