using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using DailyReport.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Infrastructure.Persistence
{
    public class ReportDailyDbContext(DbContextOptions<ReportDailyDbContext> options) : DbContext(options)
    {
        public DbSet<ReportDailyEmail> ReportDailyEmails { get; set; }
        public DbSet<ReportDailyMonitor> ReportDailyMonitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportDailyEmail>().ToTable("ReportDailyEmail");
            modelBuilder.Entity<ReportDailyEmail>().OwnsOne(p => p.EmailContent, sa =>
            {
                sa.Property(p => p.EmailAddresses).HasColumnName("EmailAddresses");
                sa.Property(p => p.Subject).HasColumnName("Subject");
                sa.Property(p => p.Body).HasColumnName("Body");
                sa.Property(p => p.AttachmentName).HasColumnName("AttachmentName");
            });
            modelBuilder.Entity<ReportDailyEmail>().ComplexProperty(p => p.ReportSchedule, sa =>
            {
                sa.Property(p => p.FreqType).HasColumnName("FreqType");
                sa.Property(p => p.FreqInterval).HasColumnName("FreqInterval").HasConversion(
                    v => (int)v,
                    v => (Weekdays)v
                );
                sa.Property(p => p.FreqSubdayType).HasColumnName("FreqSubdayType");
                sa.Property(p => p.FreqSubdayInterval).HasColumnName("FreqSubdayInterval");
                sa.Property(p => p.FreqRecurrenceFactor).HasColumnName("FreqRecurrenceFactor");
                sa.Property(p => p.ActiveStartDate).HasColumnName("ActiveStartDate");
                sa.Property(p => p.ActiveEndDate).HasColumnName("ActiveEndDate");
                sa.Property(p => p.ActiveStartTime).HasColumnName("ActiveStartTime");
                sa.Property(p => p.ActiveEndTime).HasColumnName("ActiveEndTime");
            });
            modelBuilder.Entity<ReportDailyMonitor>().ToTable("ReportDailyMonitor");

            modelBuilder.Entity<ReportDailyMonitor>().HasOne(p => p.ReportDailyEmail)
                .WithMany(p => p.ReportDailyMonitors)
                .HasForeignKey(p => p.ReportDailyEmailId);

            modelBuilder.Entity<ReportDailyEmail>().HasQueryFilter(mt => mt.Enabled);
        }
    }
}
