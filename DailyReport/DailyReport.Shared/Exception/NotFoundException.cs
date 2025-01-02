﻿namespace DailyReport.Shared.Exception;

public class NotFoundException : System.Exception
{
    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }
}
