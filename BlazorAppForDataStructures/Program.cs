using BlazorAppForDataStructures.Data;
using BlazorAppForDataStructures.Models;
using BlazorAppForDataStructures.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppForDataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration["DefaultConnection"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("DefaultConnection is not configured.");
            }

            builder.Services.AddDbContext<BlazorAppForDataStructuresContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Add any specific options here
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<BlazorAppForDataStructuresContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            });


            builder.Services.AddTransient<IEmailSender>(sp =>
            new SmtpEmailSender(
            smtpHost: "smtp.gmail.com",       // Gmail's SMTP server
            smtpPort: 587,                   // Port for TLS
            emailFrom: "datastructviz@gmail.com", // Replace with your Gmail address
            emailPassword: "jejp sdyp vigu bxvb")); // Replace with your Gmail password or App Password

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

            // Configure the HTTP request pipeline.
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