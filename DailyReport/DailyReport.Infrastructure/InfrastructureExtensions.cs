using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Repositories;
using DailyReport.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DailyReport.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IReportDailyEmailRepository), typeof(ReportDailyEmailRepository))
                .AddScoped(typeof(IReportDailyMonitorRepository), typeof(ReportDailyMonitorRepository));

            //services.AddScoped(typeof(IUnitOfWork), services =>
            //{
            //    return services.GetRequiredService<AdsDbContext>();
            //});

            //services.AddScoped<ILockManager, LockManager>();
            //services.AddScoped<ICircuitBreakerManager, CircuitBreakerManager>();

            return services;
        }
    }
}
