namespace MySpot.Api.Exceptions;

public sealed class InvalidEmployeeNameException : CustomException
{
    public InvalidEmployeeNameException() : base($"The employee name is invalid.")
    {
    }
}
