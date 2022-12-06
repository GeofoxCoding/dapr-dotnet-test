using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DAPR.Test.Frontend.Data;
using Serilog;
using DAPR.Test.Frontend.Extensions;
using Google.Api;
using System.Reflection;
using MediatR;
using DAPR.Test.ApplicationLogic.GetAllWeatherForecasts;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// LOGGER
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.File("/root/Logs/dapr_frontend_log.txt"));

// DAPR
builder.Services.AddDaprClient();

// MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(
    typeof(IRequestHandler<GetAllWeatherForecasts.Query,
        IEnumerable<GetAllWeatherForecasts.Model>>),
    typeof(GetAllWeatherForecasts.Handler));


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

