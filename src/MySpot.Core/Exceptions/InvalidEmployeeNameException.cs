namespace MySpot.Core.Exceptions;

public sealed class InvalidEmployeeNameException : CustomException
{
    public InvalidEmployeeNameException() : base($"The employee name is invalid.")
    {
    }
}
