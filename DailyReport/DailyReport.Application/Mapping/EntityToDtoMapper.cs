using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.DTOs;
using DailyReport.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace DailyReport.Application.Mapping
{
    [Mapper]
    public static partial class MapperlyMappings
    {
        [MapperIgnoreTarget (nameof(ReportDailyEmailDto.ReportDailyMonitors))]
        [MapperIgnoreSource(nameof(ReportDailyEmail.ReportDailyMonitors))]
        public static partial ReportDailyEmailDto MapToReportDailyEmailDto(ReportDailyEmail entity);
        
        [MapperIgnoreSource(nameof(ReportDailyEmailDto.ReportDailyMonitors))]
        [MapperIgnoreTarget(nameof(ReportDailyEmail.ReportDailyMonitors))]
        public static partial ReportDailyEmail MapToReportDailyEmail(ReportDailyEmailDto dto);
        
        
        public static partial ReportDailyMonitorDto MapToReportDailyMonitorDto(ReportDailyMonitor entity);
        public static partial ReportDailyMonitor MapToReportDailyMonitor(ReportDailyMonitorDto dto);
        public static partial EmailContentDto MapToEmailContentDto(EmailContent entity);
        public static partial EmailContent MapToEmailContent(EmailContentDto dto);
    }

    public static class MapperlyExtensions
    {
        public static ReportDailyEmailDto ToDto(this ReportDailyEmail entity)
        {
            return MapperlyMappings.MapToReportDailyEmailDto(entity);
        }

        public static ReportDailyEmail ToEntity(this ReportDailyEmailDto dto)
        {
            return MapperlyMappings.MapToReportDailyEmail(dto);
        }

        public static ReportDailyMonitorDto ToDto(this ReportDailyMonitor entity)
        {
            return MapperlyMappings.MapToReportDailyMonitorDto(entity);
        }

        public static ReportDailyMonitor ToEntity(this ReportDailyMonitorDto dto)
        {
            return MapperlyMappings.MapToReportDailyMonitor(dto);
        }

        public static EmailContentDto ToDto(this EmailContent entity)
        {
            return MapperlyMappings.MapToEmailContentDto(entity);
        }

        public static EmailContent ToEntity(this EmailContentDto dto)
        {
            return MapperlyMappings.MapToEmailContent(dto);
        }
    }

}
