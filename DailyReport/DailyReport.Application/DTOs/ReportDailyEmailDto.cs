using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Shared.Enums;

namespace DailyReport.Application.DTOs
{
    public class ReportDailyEmailDto : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "StoredProcedure Name is required.")]
        public string StoredProcedureName { get; set; }
        [ValidateComplexType]
        public required EmailContentDto EmailContent { get; set; }

        public bool Enabled { get; set; }
        [ValidateComplexType]
        public required ReportScheduleDto ReportSchedule { get; set; }

        public DateTime? LastProcessedDateTime { get; set; }

        public  List<ReportDailyMonitorDto>? ReportDailyMonitors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            // Validate EmailContent
            if (EmailContent != null)
            {
                var emailContentContext = new ValidationContext(EmailContent, validationContext, validationContext.Items);
                Validator.TryValidateObject(EmailContent, emailContentContext, validationResults, true);
            }
            else
            {
                validationResults.Add(new ValidationResult("EmailContent is required.", new[] { nameof(EmailContent) }));
            }

            // Validate ReportSchedule
            if (ReportSchedule != null)
            {
                var reportScheduleContext = new ValidationContext(ReportSchedule, validationContext, validationContext.Items);
                Validator.TryValidateObject(ReportSchedule, reportScheduleContext, validationResults, true);
            }
            else
            {
                validationResults.Add(new ValidationResult("ReportSchedule is required.", new[] { nameof(ReportSchedule) }));
            }

            return validationResults;
        }
    }

    public record EmailContentDto
    {
        public string? EmailAddresses { get; set; }
        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Body is required.")]
        public string Body { get; set; }
        [Required(ErrorMessage = "Attachment Name is required.")]
        public string AttachmentName { get; set; }
    }

    public record ReportScheduleDto
    {
        [Required(ErrorMessage = "Frequency Type is required.")]
        [Range(4, 16, ErrorMessage = "Invalid Frequency Type. Valid values are 4 (Daily), 8 (Weekly), 16 (Monthly).")]
        public int FreqType { get; set; }
        [Required(ErrorMessage = "Frequency Interval is required for weekly schedules.")]
        [EnumDataType(typeof(Weekdays), ErrorMessage = "Invalid Frequency Interval.")]
        public Weekdays FreqInterval { get; set; }
        [Required(ErrorMessage = "Frequency Subday Type is required.")]
        [Range(1, 8, ErrorMessage = "Invalid Subday Type. Valid values are 1 (Once), 8 (Hours).")]

        public int FreqSubdayType { get; set; }     //Determines the unit (e.g., seconds, minutes, hours).
        [Range(1, int.MaxValue, ErrorMessage = "Subday Interval must be greater than 0.")]

        public int FreqSubdayInterval { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Recurrence Factor must be greater than or equal to 0.")]

        public int FreqRecurrenceFactor { get; set; }
        [Required(ErrorMessage = "Active Start Date is required.")]
        public DateOnly ActiveStartDate { get; set; }
        [Required(ErrorMessage = "Active End Date is required.")]
        [CustomValidation(typeof(ReportScheduleDto), nameof(ValidateDateRange))]
        public DateOnly ActiveEndDate { get; set; }
        [Required(ErrorMessage = "Active Start Time is required.")]
        public TimeSpan ActiveStartTime { get; set; }
        [Required(ErrorMessage = "Active End Time is required.")]
        [CustomValidation(typeof(ReportScheduleDto), nameof(ValidateTimeRange))]
        public TimeSpan ActiveEndTime { get; set; }

        public static ValidationResult ValidateDateRange(DateOnly endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as ReportScheduleDto;
            if (instance != null && instance.ActiveStartDate > endDate)
            {
                return new ValidationResult("Active End Date must be later than Start Date.");
            }
            return ValidationResult.Success!;
        }

        public static ValidationResult ValidateTimeRange(TimeSpan endTime, ValidationContext context)
        {
            var instance = context.ObjectInstance as ReportScheduleDto;
            if (instance != null && instance.ActiveStartTime >= endTime)
            {
                return new ValidationResult("Active End Time must be later than Start Time.");
            }
            return ValidationResult.Success!;
        }
    }
}
