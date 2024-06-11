using FlightOperator.Service;

public class FlightDelayService
{
    public Dictionary<int, Flight> Flights { get; private set; } = []; //bármilyen tömb/lista stb.... [] new helyett


    public void CreatingNewFlight(string? arrival, string? departure)
    {
        Guid flightNum = Guid.NewGuid();
        DateTime departureTime = DateTime.Now;

        if (IsString(arrival) && IsString(departure))
        {
            Flight flight = new(flightNum, departure, arrival, departureTime);
            NewRoute(flight);
        }
    }

    public void NewRoute(Flight flight)
    {
        int listSize = Flights.Count;
        flight.Id = listSize;
        Flights.Add(listSize, flight);
    }

    public string DelayFlight(string? stringIndex, string? delaymin)
    {
        try
        {
            if (IsInt(stringIndex) && IsInt(delaymin))
            {
                int intIndex = Convert.ToInt32(stringIndex);
                int delayminute = Convert.ToInt32(delaymin);
                Flights[intIndex].Departure = Flights[intIndex].Departure.AddMinutes(delayminute);
                Flights[intIndex].Delay = delayminute;
                return "Flight is succesfully updated!";
            }
            return "Choose an exist flight to update!";
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException("Wrong index!");
        }
        catch (Exception ex)
        {
            return "Something went wrong";
        }
    }

    public string FlightDepartureTime(string? id)
    {
        if (IsInt(id))
        {
            int intId = Convert.ToInt32(id);
            return $"{Flights[intId].Departure}";
        }
        return "Invalid flight";
    }

    public string GetFlightsByAirport(string? stringIndex)
    {
        if (IsInt(stringIndex))
        {
            List<string> list = new();

            int index = Convert.ToInt32(stringIndex);
            string searchedAirport = Flights[index].From;

            list = Flights.Values.Where(item => item.From == searchedAirport)
                                 .Select(x => Convert.ToString(x.FlightNumber) ?? string.Empty)//??- 0 érték
                                 .ToList();
            return string.Join("\n", list);
        }
        return "The airport does not exist!";
    }

    #region Private methods
    private bool IsInt(string? stringIndex)
    {
        if (int.TryParse(stringIndex, out var index))
        {
            return true;
        }
        return false;
    }

    private bool IsString(string? givenString)
    {
        if (string.IsNullOrWhiteSpace(givenString))
        {
            return false;
        }
        return true;
    }
    #endregion
}