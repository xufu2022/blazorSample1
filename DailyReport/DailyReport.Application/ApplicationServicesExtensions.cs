using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DailyReport.Application
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            Action<Type, Type, ServiceLifetime> configureInterceptor = null)
        {
            services.AddScoped<IReportService, ReportService>();
            return services;
        }
    }
}
