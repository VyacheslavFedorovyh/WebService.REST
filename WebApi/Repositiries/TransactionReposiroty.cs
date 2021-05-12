using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Data;
using WebApi.Exeptions;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.ViewModels;

namespace WebApi.Repositiries
{
	public class TransactionReposiroty : ITransactionRepository
	{
		private DB_TransactionContext _context;
		public TransactionReposiroty(DB_TransactionContext db)
		{
			_context = db;
		}

		public List<TransactionView> GetAll()
		{
			try
			{
				var result = _context.Transactions.Select(s =>
				new TransactionView()
				{
					Amount = s.Amount,
					Notes = s.Notes,
					TransactionTime = s.TransactionTime,
					UserId = s.UserId,
					TransactionId = s.TransactionID
				})
					.ToList();

				return result;
			}
			catch (Exception exc)
			{
				throw new AppExeption(exc.Message, exc);

			}
		}

		public int? GetBalanceByUser(long? userId)
		{
			try
			{
				var user = _context.Users.FirstOrDefault(w => w.UserID == userId.Value);
				if (user == null) { throw new AppExeption("Пользователь не найден."); }
				return user.Account;
			}
			catch (Exception exc)
			{
				throw new AppExeption(exc.Message, exc);

			}
		}

		public List<TransactionView> HistoryTransaction(long? userID, DateTime? fromDate, DateTime? toDate)
		{
			try
			{
				var result = _context.Transactions
					.Where(w => (userID != null ? w.User.UserID == userID.Value : false)
					&& (fromDate != null ? w.TransactionTime.Date >= fromDate.Value.Date : false)
					&& (toDate != null ? w.TransactionTime.Date <= toDate.Value.Date : false)).Select(s =>
					new TransactionView()
					{
						Amount = s.Amount,
						Notes = s.Notes,
						TransactionTime = s.TransactionTime,
						UserId = s.UserId,
						TransactionId = s.TransactionID
					}).ToList();

				if (result.Count == 0) { throw new AppExeption("История операций по заданным параметрам не найдена."); }
				return result;
			}
			catch (Exception exc)
			{
				throw new AppExeption(exc.Message, exc);
			}
		}

		public List<TransactionView> StatisticTransaction(long? userID, DateTime? date)
		{
			try
			{
				var result = _context.Transactions
					.Where(w => (userID != null ? w.User.UserID == userID.Value : false)
					&& (date != null ? DateTime.Compare(w.TransactionTime, date.Value) == 0 : false)).Select(s =>
					new TransactionView()
					{
						Amount = s.Amount,
						Notes = s.Notes,
						UserId = s.User.UserID,
						TransactionTime = s.TransactionTime,
						TransactionId = s.TransactionID
					}).ToList();

				if (result.Count == 0) { throw new AppExeption("История операций по заданным параметрам не найдена."); }
				return result;
			}
			catch (Exception exc)
			{
				throw new AppExeption(exc.Message, exc);
			}
		}

		public TransactionStatisticView StatisticTransactionAll(long? userID, DateTime? date)
		{
			try
			{
				var result = new TransactionStatisticView();
				var transactions = _context.Transactions
					.Where(w => (userID != null ? w.User.UserID == userID.Value : false)
					&& (date != null ? DateTime.Compare(w.TransactionTime, date.Value) == 0 : false)).Select(s =>
					new TransactionView()
					{
						UserId = s.UserId,
						Amount = s.Amount
					}).ToList();

				if (transactions.Count == 0) { throw new AppExeption("История операций по заданным параметрам не найдена."); }

				result.UserId = transactions.FirstOrDefault().UserId;

				foreach (var transaction in transactions)
				{
					if (transaction.Amount > 0)
						result.AmountIn += transaction.Amount;
					else
						result.AmountOut += transaction.Amount;
				}

				return result;
			}
			catch (Exception exc)
			{
				throw new AppExeption(exc.Message, exc);
			}
		}

		public List<TransactionView> StatisticTransaction(DateTime? date, bool operation)
		{
			try
			{
				var result = _context.Transactions
					.Where(w => (date != null ? DateTime.Compare(w.TransactionTime, date.Value) == 0 : false)
					&& (operation ? w.Amount > 0 : w.Amount < 0))
					.Select(s =>
					new TransactionView()
					{
						Amount = s.Amount,
						Notes = s.Notes,
						UserId = s.User.UserID,
						TransactionTime = s.TransactionTime,
						TransactionId = s.TransactionID
					}).ToList();

				if (result.Count == 0) { throw new AppExeption("История операций по заданным параметрам не найдена."); }
				return result;
			}
			catch (Exception exc)
			{
				throw new AppExeption(exc.Message, exc);
			}
		}

		public string AddTransaction(long? userID, int amount, string notes)
		{
			try
			{
				var user = _context.Users.FirstOrDefault(i => i.UserID == userID);
				var transaction = new Transaction();
				var transactionTime = DateTime.Today;

				if (user != null)
				{
					var account = user.Account + amount;
					if (account < 0)
						throw new AppExeption("Недостаточно средств на счету.");

					transaction = new Transaction(transactionTime, userID.Value, notes, account);
					_context.Transactions.Add(transaction);
					user.Account = account;
					_context.SaveChanges();
				}
				else
					throw new AppExeption("Пользователь не найден.");

				return "Транзакция успешна. Ид транзакции: " + transaction.TransactionID;
			}
			catch (Exception exc)
			{
				throw new AppExeption(exc.Message, exc);
			}
		}
	}
}
