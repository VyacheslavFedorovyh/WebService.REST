using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class TransactionView
    { 
        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>        
        public string Notes { get; set; }
        /// <summary>
        /// Сумма транзакции
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Ид транзакции
        /// </summary>
        public Guid TransactionId { get; set; }    
    }
}
