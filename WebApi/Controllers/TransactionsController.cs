using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/")]
	public class TransactionsController : ControllerBase
	{
		private readonly ITransactionRepository _methods;
		public TransactionsController(ITransactionRepository payment)
		{
			_methods = payment;
		}

		#region GET

		[HttpGet("Transactions")]
		[CustomExceptionFilterAttribute]
		public IActionResult GetAll()
		{
			var data = _methods.GetAll();
			return Ok(data);
		}

		// GET: Balance?userID=3
		[HttpGet("Balance")]
		[CustomExceptionFilterAttribute]
		public IActionResult Balance(long? userID)
		{
			var data = _methods.GetBalanceByUser(userID);
			return Ok(data);
		}

		// GET: History?userID=3&fromDate=2021-01-04&toDate=2021-01-08
		[HttpGet("History")]
		[CustomExceptionFilterAttribute]
		public IActionResult History(long? userID, DateTime? fromDate, DateTime? toDate)
		{
			var data = _methods.HistoryTransaction(userID, fromDate, toDate);
			return Ok(data);
		}

		#region Statistic

		// GET: Statistic?userID=3&date=2021-01-06
		[HttpGet("Statistic")]
		[CustomExceptionFilterAttribute]
		public IActionResult Statistic(long? userID, DateTime? date)
		{
			var data = _methods.StatisticTransaction(userID, date);
			return Ok(data);
		}

		// GET: StatisticIn?inDate=2021-01-06
		[HttpGet("StatisticIn")]
		[CustomExceptionFilterAttribute]
		public IActionResult StatisticIn(DateTime? inDate)
		{
			var data = _methods.StatisticTransaction(inDate, true);
			return Ok(data);
		}

		// GET: StatisticOut?outDate=2021-01-06
		[HttpGet("StatisticOut")]
		[CustomExceptionFilterAttribute]
		public ActionResult<int> StatisticOut(DateTime? outDate = null)
		{
			var data = _methods.StatisticTransaction(outDate, false);
			return Ok(data);
		}

		#endregion Statistic 

		#endregion GET

		#region POST

		// POST: AddTransaction?userID=3&amount=3000&notes=Снятие
		[HttpPost("AddTransaction")]
		[CustomExceptionFilterAttribute]
		public IActionResult AddTransaction(long? userID, int? amount, string notes)
		{
			if (amount == null && amount == 0)
				return NotFound("Не указана сумма");

			var data = _methods.AddTransaction(userID, amount.Value, notes);
			return Ok(data);
		}

		#endregion POST
	}
}
