using FlightOperator.Service;

public class FlightDelayService
{
    private Dictionary<int, Flight> flights = []; //bármilyen tömb/lista stb.... [] new helyett

    public void NewRoute(Flight flight)
    {
        int listSize = flights.Count;
        flight.Id = listSize;
        flights.Add(listSize, flight);
    }

    public bool isString(string? givenString)
    {
        if (string.IsNullOrWhiteSpace(givenString))
        {
            return false;
        }
        return true;
    }

    public string DelayFlight(string? stringIndex, string? delaymin)
    {
        try
        {
            if (IsInt(stringIndex) && IsInt(delaymin))
            {
                int intIndex = Convert.ToInt32(stringIndex);
                int delayminute = Convert.ToInt32(delaymin);
                flights[intIndex].Departure = flights[intIndex].Departure.AddMinutes(delayminute);
                flights[intIndex].Delay = delayminute;
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

    public Dictionary<int, Flight> GetFlights()
    {
        return flights;
    }

    public bool IsInt(string? stringIndex)
    {
        if (int.TryParse(stringIndex, out var index))
        {
            return true;
        }
        return false;
    }

    public string FlightDepartureTime(string? id)
    {
        if (IsInt(id))
        {
            int intId = Convert.ToInt32(id);
            return $"{flights[intId].Departure}";
        }
        return "Invalid flight";
    }

    private List<string> FlightsFromAirport(string givenId)
    {
        return flights.Values.Where(item => item.Id.ToString() == givenId)//linq//nem id hane reptér
                             .Select(x => x.FlightNumber.ToString())
                             .ToList();
        /* List<string> list = new();

         foreach (KeyValuePair<int, Flight> kvp in flights)
         {
             if (kvp.Value.From == airport)
             {
                 list.Add(kvp.Value.FlightNumber.ToString());
             }
         }*/
    }

    public string ListToConsole(string airport)
    {
        List<string> list = FlightsFromAirport(airport);
        string text = "";

        foreach (var item in list)
        {
            text += $"{item} {Environment.NewLine}";
        }

        return text;
    }
}