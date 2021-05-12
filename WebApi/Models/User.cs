using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
	public class User
	{
		/// <summary>
		/// Уникальный Идентификатор пользователя
		/// </summary>
		[Key]
		public long UserID { get; set; }

		/// <summary>
		/// Средства пользователя
		/// </summary>
		public int Account { get; set; }

		public virtual List<Transaction> Transactions { get; set; } = new List<Transaction>();
	}
}
