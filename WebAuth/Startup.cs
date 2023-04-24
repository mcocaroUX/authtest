using Microsoft.AspNetCore.Authentication.Cookies;

namespace AuthTest.WebAuth
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			Configuration = configuration;
			WebHostEnvironment = webHostEnvironment;
		}

		IConfiguration Configuration { get; }

		IWebHostEnvironment WebHostEnvironment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddAuthentication(o =>
				{
					o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				.AddCookie()
				.AddGoogle(g =>
				{
					IConfigurationSection googleAuthNSection =
					Configuration.GetSection("Authentication:Google");

					g.ClientId = googleAuthNSection["ClientId"];
					g.ClientSecret = googleAuthNSection["ClientSecret"];
					g.SaveTokens = true;
				});
			//.AddApple(a =>
			//{
			//	a.ClientId = Configuration["AppleClientId"];
			//	a.KeyId = Configuration["AppleKeyId"];
			//	a.TeamId = Configuration["AppleTeamId"];
			//	a.UsePrivateKey(keyId
			//		=> WebHostEnvironment.ContentRootFileProvider.GetFileInfo($"AuthKey_{keyId}.p8"));
			//	a.SaveTokens = true;
			//});

			/*
            * For Apple signin
            * If you are running the app on Azure App Service you must add the Configuration setting
            * WEBSITE_LOAD_USER_PROFILE = 1
            * Without this setting you will get a File Not Found exception when AppleAuthenticationHandler tries to generate a certificate using your AuthKey_{keyId}.p8 file.
            */
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
