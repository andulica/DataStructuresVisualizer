using BlazorAppForDataStructures.Data;
using BlazorAppForDataStructures.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace BlazorAppForDataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("DefaultConnection is not configured.");
            }

            builder.Services.AddDbContext<BlazorAppForDataStructuresContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<BlazorAppForDataStructuresContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            });


            builder.Services.AddTransient<IEmailSender>(sp =>
            new SmtpEmailSender(
            smtpHost: "smtp.gmail.com",       
            smtpPort: 587,                   
            emailFrom: "datastructviz@gmail.com",
            emailPassword: "jejp sdyp vigu bxvb"));

            builder.Services.AddScoped<QuizService>();
            builder.Services.AddScoped<CancellationService>();
            builder.Services.AddScoped<SecureStorageService>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            builder.Services.AddScoped<NotificationService>();


            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<HttpClient>(sp =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://datastructviz-quiz-api-001-hbcza9gdbpb7gzew.canadacentral-01.azurewebsites.net/")
                };

                return httpClient;
            });

            builder.Configuration.AddUserSecrets<Program>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}