using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TailwindBlazorElectron.Services;
using ElectronNET.API.Entities;
using ElectronNET.API;
using TailwindBlazorElectron.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TailwindBlazorElectron
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddElectron();
			services.AddRazorPages(o => o.RootDirectory = "/Views");
			services.AddServerSideBlazor();

			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer"),
																   b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)),
																   ServiceLifetime.Singleton);

			services.AddSingleton<BookService>();
			services.AddSingleton<AuthorService>();
			services.AddSingleton<LibraryService>();

			services.AddSingleton<WindowService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
		{
			dbContext.Database.Migrate();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				// app.UseHsts();
			}

			// app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});

			if (HybridSupport.IsElectronActive)
			{
				ElectronBootstrap();
			}
		}

		public void ElectronBootstrap()
		{
			Task.Run(async () =>
			{
				var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
				{
					Width = 1600,
					Height = 900,
					Show = false,
					Frame = false

				});

				browserWindow.SetMenuBarVisibility(false);
				browserWindow.SetTitle("BookShelf");
				browserWindow.OnReadyToShow += () => browserWindow.Show();
			});
		}
	}
}
