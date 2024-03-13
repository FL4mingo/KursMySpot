using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

public class Reservation
{
    public int Id { get; }
    public string EmployeeName { get; private set; }
    public string LicensePlate { get; private set; }
    public DateTime Date { get; private set; }

    public Reservation(int id, string employeeName, string licensePlate, DateTime date)
    {
        Id = id;
        EmployeeName = employeeName;
        ChangeLicensePlate(licensePlate);
        Date = date;
    }

    public void ChangeLicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new EmptyLicensePlateException();
        }

        LicensePlate = licensePlate;
    }
}
