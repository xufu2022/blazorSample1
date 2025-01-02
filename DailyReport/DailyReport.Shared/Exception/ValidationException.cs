namespace DailyReport.Shared.Exception;

public class ValidationException : System.Exception
{
    public static void Requires(bool expected, string errorMessage)
    {
        if (!expected)
        {
            throw new ValidationException(errorMessage);
        }
    }

    public ValidationException(string message)
        : base(message)
    {
    }

    public ValidationException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}