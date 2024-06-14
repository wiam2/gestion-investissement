
using ChatApplicationYT.Hub;
using MicroSAuth_GUser.Data;
using MicroServiceMessagerie.Hub;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceMessagerie.Models;
using Steeltoe.Discovery.Client;
using System.Net.Http;


namespace ServiceMessagerie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IDictionary<string, Conversation>>(opt =>
                new Dictionary<string, Conversation>());
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            builder.Services.AddTransient<ChatHub>(provider => new ChatHub(new Dictionary<string, Conversation>(), provider.GetRequiredService<AppDbContext>(),  provider.GetRequiredService<HttpClient>()));
            builder.Services.AddScoped<ConversationService>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("Connection string is not found"));
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddDiscoveryClient(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoint =>
             {
                 endpoint.MapHub<ChatHub>("/chat");
                 endpoint.MapControllers();
             });

            app.MapControllers();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.Run();





        }
    }
}

