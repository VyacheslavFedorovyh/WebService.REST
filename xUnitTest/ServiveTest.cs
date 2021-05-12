using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using WebApi;
using Xunit;

namespace xUnitTest
{
	public class ServiveTest: IClassFixture<TestStartup<Startup>>
	{
		private HttpClient Client;

		public ServiveTest(TestStartup<Startup> fixture)
		{
			Client = fixture.Client;
		}

		[Fact]
		public async Task TestGetBalance()
		{
			// Arrange
			int account = 240000;			
			var request = "/api/Balance?userID=3";

			// Act
			var response = await Client.GetAsync(request);
			var amountQuestion = int.Parse(response.Content.ReadAsStringAsync().Result, CultureInfo.InvariantCulture);

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.True(account.Equals(amountQuestion));
		}

		[Fact]
		public async Task TestGetHistoryTransaction()
		{
			// Arrange
			int answer = 4;
			var request = "/api/History?userID=3&fromDate=2021-01-04&toDate=2021-01-08";

			// Act
			var response = await Client.GetAsync(request);
			var answerQuestion = response.Content.ReadAsStringAsync().Result.Remove(0, 2).Split('{');

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.True(answer.Equals(answerQuestion.Count()));
		}

		[Fact]
		public async Task TestPostAddTransaction()
		{
			// Arrange           
			var request = "/api/AddTransaction?userID=1&amount=3000&notes=Снятие";

			// Act
			var response = await Client.PostAsync(request, ContentHelper.GetStringContent(""));

			// Assert
			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task TestGetStatisticTransaction()
		{
			// Arrange
			string answer = "[{\"transactionTime\":\"2021-04-01T00:00:00\",\"notes\":\"Чаевые\",\"amount\":600,\"userId\":5,\"transactionId\":\"5e69707b-5a13-4361-99d8-caacee037c06\"}]";
			var request = "/api/Statistic?userID=5&date=2021-04-01";

			// Act
			var response = await Client.GetAsync(request);
			var answerQuestion = response.Content.ReadAsStringAsync().Result;

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.True(answer.Equals(answerQuestion));
		}
		
		[Fact]
		public async Task TestGetStatisticTransactionNotData()
		{
			// Arrange			
			var request = "/api/Statistic?userID=5&onDate=";

			// Act
			var response = await Client.GetAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task TestGetStatisticTransactionIn()
		{
			// Arrange
			var request = "/api/StatisticIn?inDate=2021-03-02";

			// Act
			var response = await Client.GetAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task TestGetStatisticTransactionOut()
		{
			// Arrange
			var request = "/api/StatisticOut?outDate=2021-01-06";

			// Act
			var response = await Client.GetAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
		}

	}
}
