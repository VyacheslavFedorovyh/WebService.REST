using System;
using System.Linq;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
	public class DB_TransactionContext : DbContext
	{
		public DB_TransactionContext(DbContextOptions<DB_TransactionContext> options) : base(options)
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
			   .HasMany(c => c.Transactions)
			   .WithOne(o => o.User)
			   .HasForeignKey(h => h.UserId);

			modelBuilder.Entity<User>().HasData(
			new User[]
			{
				new User{UserID=1,Account=20000},
				new User{UserID=2,Account=140000},
				new User{UserID=3,Account=240000},
				new User{UserID=4,Account=165000},
				new User{UserID=5,Account=26000},
				new User{UserID=6,Account=59000},
				new User{UserID=7,Account=34000}
			});

			modelBuilder.Entity<Transaction>().HasData(
			new Transaction[]
			{
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-03-02"),Amount=-2400,Notes = "Оплата долга",UserId = 1},
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-01-06"),Amount=-15000,Notes = "Оплата ресторана",UserId= 3},
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-01-06"),Amount=-16000,Notes = "Покупка товара",UserId= 3},
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-01-06"),Amount=24000,Notes = "Перевод другу",UserId= 3},
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-01-06"),Amount=6000,Notes = "Перевод жене",UserId= 3},
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-03-06"),Amount=5000,Notes = "Спасибо",UserId= 4},
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-05-04"),Amount=-20000,Notes = "Покупка техники",UserId= 2},
				new Transaction() {TransactionID= Guid.NewGuid(),TransactionTime=DateTime.Parse("2021-05-02"),Amount=-50,Notes = "Проезд",UserId= 3},
				new Transaction() {TransactionID= new System.Guid("5e69707b-5a13-4361-99d8-caacee037c06"),TransactionTime=DateTime.Parse("2021-04-01"),Amount=600,Notes = "Чаевые",UserId= 5}
			});

		}
	}
}
