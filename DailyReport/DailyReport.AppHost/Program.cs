var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DailyReport>("dailyreport");

builder.AddProject<Projects.DailyReport_API>("dailyreport-api");

builder.AddProject<Projects.DailyReport_Web_UI_Samples>("dailyreport-web-ui-samples");

builder.Build().Run();
