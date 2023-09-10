using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace InfoEndpoint
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapGet("/api", (string slack_name, string track) =>
            {
                string current_day = DateTime.Now.ToString("dddd");
                DateTime current_utc_time = DateTime.UtcNow.AddMinutes(new Random());

                var response = new
                {
                    slack_name = slack_name,
                    current_day = current_day,
                    utc_time = current_utc_time.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    track = track,
                    github_file_url = "https://github.com/RaphaelOmeike/stageshngx/blob/main/Program.cs",
                    github_repo_url = "https://github.com/RaphaelOmeike/stageshngx",
                    status_code = (int)HttpStatusCode.OK
                };

                return Results.Json(response);
            });

            app.Run();
        }
    }
    

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    [Route("api")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInfo([FromQuery] string slack_name, [FromQuery] string track)
        {
            // Get current day of the week
            string current_day = DateTime.Now.ToString("dddd");

            // Get current UTC time within +/-2 minutes
            DateTime current_utc_time = DateTime.UtcNow.AddMinutes(new Random().Next(-2, 3));

            var response = new
            {
                slack_name = "Omeike Raphael Ehije",
                current_day = current_day,
                utc_time = current_utc_time.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                track = "backend",
                github_file_url = "https://github.com/username/repo/blob/main/file_name.ext",
                github_repo_url = "https://github.com/username/repo",
                status_code = (int)HttpStatusCode.OK
            };

            return Ok(response);
        }
    }
}
