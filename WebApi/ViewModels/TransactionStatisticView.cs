using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
	public class TransactionStatisticView
	{
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Сумма приходов
        /// </summary>
        public int AmountIn { get; set; }
        /// <summary>
        /// Сумма расходов
        /// </summary>
        public int AmountOut { get; set; }

    }
}
