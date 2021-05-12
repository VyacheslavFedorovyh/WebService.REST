using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Repositiries;

namespace WebApi
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		private readonly IWebHostEnvironment _currentEnvironment;

		public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
		{
			Configuration = configuration;
			_currentEnvironment = currentEnvironment;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddMvc();
			services.AddDbContext<DB_TransactionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddTransient<ITransactionRepository, TransactionReposiroty>();	
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
