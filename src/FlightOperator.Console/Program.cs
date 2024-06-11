using FlightOperator.Service;
public class Program
{
    private static void Main(string[] args)
    {
        Menu();
    }

    private static void Menu()
    {
        FlightDelayService service = new();

        Console.WriteLine("====================================================");
        Console.WriteLine("                   Manage Flights                   ");
        Console.WriteLine("====================================================");

        while (true)
        {
            try
            {
                Console.WriteLine("\n 1) Add flight          4) Get departure time");
                Console.WriteLine(" 2) Check all flights   5) Get flights from airport");
                Console.WriteLine(" 3) Delay a flight      6)  End program\n");

                string answer = Console.ReadLine();
                Console.Write("\n");

                if (answer == "1")
                {
                    NewFlight(service);
                }
                else if (answer == "2")
                {
                    GetFlightsFromList(service);
                }
                else if (answer == "3")//negativnál ne delay legyen, behozhatja az időt
                {
                    Delay(service);
                }
                else if (answer == "4")
                {
                    DepartureTime(service);
                }
                else if (answer == "5")
                {
                    GetAirports(service);
                }
                else if (answer == "6")
                {
                    break;
                }
                else
                {
                    ConsoleError("Invalid input number!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    private static void ConsoleError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void GetFlightsFromList(FlightDelayService service)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        foreach (KeyValuePair<int, Flight> kvp in service.Flights)
        {
            Console.WriteLine($"{kvp.Key} {kvp.Value}");
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void NewFlight(FlightDelayService service)
    {
        Console.Write("Flight's data:\ndeparture: ");
        string? dep = Console.ReadLine();
        Console.Write("arrive: ");
        string? arr = Console.ReadLine();

        service.CreatingNewFlight(dep, arr);
    }

    private static void Delay(FlightDelayService service)
    {
        string? selectedFlight = SelectAFlight(service);

        Console.Write("Flight is delayed with this much minutes: ");
        string? minute = Console.ReadLine();

        Console.WriteLine(service.DelayFlight(selectedFlight, minute));
    }

    private static string? SelectAFlight(FlightDelayService service)
    {
        GetFlightsFromList(service);
        Console.Write("\nSelect flight: ");
        string? selectedFlight = Console.ReadLine();

        return selectedFlight;
    }

    public static void DepartureTime(FlightDelayService service)
    {
        string? stringId = SelectAFlight(service);
        var result = service.FlightDepartureTime(stringId);

        Console.WriteLine(result);
    }

    public static void GetAirports(FlightDelayService service)
    {
        string? stringIndex = SelectAFlight(service);
        var result = service.GetFlightsByAirport(stringIndex);
        Console.WriteLine(result);
    }
}