using DailyReport.Application.Services;
using DailyReport.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IReportService, ClientReportService>();

await builder.Build().RunAsync();
