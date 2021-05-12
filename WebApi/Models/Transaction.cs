using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
	public class Transaction
	{
        public Transaction() { }

		public Transaction(Transaction s)
		{
			TransactionID = s.TransactionID;
			TransactionTime = s.TransactionTime;
			UserId = s.UserId;
			Notes = s.Notes;
			Amount = s.Amount;
		}

		public Transaction(DateTime transactionTime, long userId, string notes, int amount)
		{
			TransactionID = Guid.NewGuid();
			TransactionTime = transactionTime;
			UserId = userId;
			Notes = notes;
			Amount = amount;
		}

		/// <summary>
		/// ИД Транзакции
		/// </summary>
		[Key]
        public Guid TransactionID { get; set; }
        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Сумма транзакции
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        [MaxLength(200)]
        public string Notes { get; set; }
		/// <summary>
		/// Ид пользователя
		/// </summary>
		public long UserId { get; set; }

		public virtual User User { get; set; }
	}
}
