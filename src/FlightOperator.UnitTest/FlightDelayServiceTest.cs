namespace FlightOperator.UnitTest;
using FlightOperator.Service;
using FluentAssertions;
public class FlightDelayServiceTest
{
    private readonly FlightDelayService _service;

    public FlightDelayServiceTest()
    {
        _service = new FlightDelayService();
    }

    [Fact]
    public void NewRoute_AddedToDictionary_ReturnAddedFlightToDictionary()
    {
        //Arrange
        var flight = new Flight(new Guid(), "Budapest", "Wien", DateTime.Now);

        //Act
        _service.NewRoute(flight);

        //Assert
        _service.GetFlights().Should().HaveCount(1);
        _service.GetFlights()[0].Should().Be(flight);
    }

    [Fact]
    public void NewRoute_AddedToDictionary_ReturnRightSizes()
    {
        //Arrange
        var oldSize = _service.GetFlights().Count();
        var flight = new Flight(new Guid(), "Budapest", "Wien", DateTime.Now);

        //Act
        _service.NewRoute(flight);
        var newSize = _service.GetFlights().Count();

        //Assert
        newSize.Should().BeGreaterThan(oldSize);
        newSize.Should().Be(oldSize + 1);
    }

    [Theory]
    [InlineData("2")]
    [InlineData("-7")]
    [InlineData("0")]
    public void DelayFlight_CorrectParameters_ReturnSuccesString(string delaymin)
    {
        //Arrange 
        var flight = new Flight(new Guid(), "Budapest", "Wien", DateTime.Now);

        //Act
        _service.NewRoute(flight);
        var result = _service.DelayFlight("0", delaymin);

        //Assert
        result.Should().Be("Flight is succesfully updated!");
    }

    [Theory]
    [InlineData("sziaa")]
    [InlineData("2h")]
    [InlineData("")]
    public void DelayFlight_CorrectParameters_ReturnErrorString(string delaymin)
    {
        //Arrange 
        var flight = new Flight(new Guid(), "Budapest", "Wien", DateTime.Now);

        //Act
        _service.NewRoute(flight);
        var result = _service.DelayFlight("0", delaymin);

        //Assert
        result.Should().Be("Choose an exist flight to update!");
    }

    [Fact]
    public void DelayFlight_CorrectParameters_ReturnIndexOutOfRange()
    {
        //Arrange 
        var flight = new Flight(new Guid(), "Budapest", "Wien", DateTime.Now);

        //Act
        _service.NewRoute(flight);
        Action result = () => _service.DelayFlight("100", "2");

        //Assert
        result.Should().Throw<KeyNotFoundException>()
              .WithMessage("Wrong index!");
    }

}