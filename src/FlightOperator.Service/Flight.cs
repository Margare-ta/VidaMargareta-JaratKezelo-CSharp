namespace FlightOperator.Service;

public class Flight
{
    // All prop is upper case
    public int Id { get; set; }
    public Guid FlightNumber { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime Departure { get; set; }
    public double Delay { get; set; }

    public Flight(Guid flightNumber, string from, string to, DateTime departure)
    {
        this.FlightNumber = flightNumber;
        this.From = from;
        this.To = to;
        this.Departure = departure;
        this.Delay = 0;
    }

    public override string? ToString()
    {
        if (Delay == 0)
        {
            return $"{FlightNumber}: from {From} to {To} at {Departure} will departure.";
        }
        return $"{FlightNumber}: from {From} to {To} at {Departure} has {Delay} minute(s) delay.";
    }
}