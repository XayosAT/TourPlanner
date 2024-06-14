using TourPlanner;
using TourPlanner.Enums;
using TourPlanner.Helpers;
using TourPlanner.Models;
using TourPlanner.RESTServices;

namespace TourPlannerUnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    // test if the demo tour log is created correctly
    public void Test1()
    {
        TourLogs expectedLog = new TourLogs();
        
        expectedLog.Date = DateTime.Now.ToString("dd.MM.yyyy");
        expectedLog.TotalTime = 5;
        expectedLog.Comment = "Demo Comment";
        expectedLog.TotalDistance = 3;
        
        TourLogs newLog = new TourLogs();
        Tour tour = new Tour();
        newLog = tour.GetDemoLog();
        
        Assert.That(newLog.Date, Is.EqualTo(expectedLog.Date));
        Assert.That(newLog.TotalTime, Is.EqualTo(expectedLog.TotalTime));
        Assert.That(newLog.Comment, Is.EqualTo(expectedLog.Comment));
        Assert.That(newLog.TotalDistance, Is.EqualTo(expectedLog.TotalDistance));
    }
    
    [Test]
    // test if username is set in correct format
    public void Test2()
    {
        var username = "Random123";
        Tour tour = new Tour();
        tour.Name = username;
        bool isValid = tour.HasValidInput();
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    // test if tour parameters are set correctly
    public void Test3()
    {
        Tour tour = new Tour();
        tour.Description = "Test Description";
        tour.Name = "Test Name";
        tour.StartLocation = "Start Location";
        tour.EndLocation = "End Location";
        tour.Distance = 100;
        tour.Time = 60;
        tour.ImagePath = "Path to Image";
        
        Assert.That(tour.Description, Is.EqualTo("Test Description"));
        Assert.That(tour.Name, Is.EqualTo("Test Name"));
        Assert.That(tour.StartLocation, Is.EqualTo("Start Location"));
        Assert.That(tour.EndLocation, Is.EqualTo("End Location"));
        Assert.That(tour.Distance, Is.EqualTo(100));
        Assert.That(tour.Time, Is.EqualTo(60));
        Assert.That(tour.ImagePath, Is.EqualTo("Path to Image"));
    }
    
    [Test]
    // test if tour log parameters are set correctly
    public void Test4()
    {
        TourLogs logs = new TourLogs();
        logs.Date = "01.01.2021";
        logs.TotalTime = 5;
        logs.Comment = "Test Comment";
        logs.TotalDistance = 100;
        
        Assert.That(logs.Date, Is.EqualTo("01.01.2021"));
        Assert.That(logs.TotalTime, Is.EqualTo(5));
        Assert.That(logs.Comment, Is.EqualTo("Test Comment"));
        Assert.That(logs.TotalDistance, Is.EqualTo(100));
    }
    
    [Test]
    // test tour log rating
    public void Test5()
    {
        TourLogs logs = new TourLogs();
        logs.Rating = Rating.Good;
        
        Assert.That(logs.Rating, Is.EqualTo(Rating.Good));
    }
    
    [Test]
    // test tour log difficulty
    public void Test6()
    {
        TourLogs logs = new TourLogs();
        logs.Difficulty = Difficulty.Medium;
        
        Assert.That(logs.Difficulty, Is.EqualTo(Difficulty.Medium));
    }
    
    [Test]
    // test if tour log is added correctly
    public void Test7()
    {
        Tour tour = new Tour();
        tour.AddDemoLogs();
        
        Assert.That(tour.Logs.Count, Is.GreaterThan(0));
    }
    
    [Test]
    // test if tour is cleared correctly
    public void Test8()
    {
        Tour tour = new Tour();
        tour.Description = "Test Description";
        tour.Name = "Test Name";
        tour.StartLocation = "Start Location";
        tour.EndLocation = "End Location";
        tour.Distance = 100;
        tour.Time = 60;
        tour.ImagePath = "Path to Image";
        
        tour.Clear();
        
        Assert.That(tour.Description, Is.EqualTo(""));
        Assert.That(tour.Name, Is.EqualTo(""));
        Assert.That(tour.StartLocation, Is.EqualTo(""));
        Assert.That(tour.EndLocation, Is.EqualTo(""));
        Assert.That(tour.Distance, Is.EqualTo(0));
        Assert.That(tour.Time, Is.EqualTo(0));
        Assert.That(tour.ImagePath, Is.EqualTo(""));
    }
    
    [Test]
    // test if coordinates are retrieved correctly
    public void Test9()
    {
        RouteService routeService = new RouteService();
        routeService.Configure("5b3ce3597851110001cf624814f69a36c67c4473a3ae8cdd7b8e79e1");
        var coordinates = routeService.GetCoordinatesAsync("Berlin").Result;
        
        // lat
        Assert.That(coordinates.Item1, Is.EqualTo("52.524932"));
        // long
        Assert.That(coordinates.Item2, Is.EqualTo("13.407032"));
    }
    
    [Test]
    // test if route information is retrieved without errors
    public void Test10()
    {
        RouteService routeService = new RouteService();
        routeService.Configure("5b3ce3597851110001cf624814f69a36c67c4473a3ae8cdd7b8e79e1");
        var jsonResponse = routeService.GetDirAsync("Berlin", "Munich").Result;
        
        Assert.That(jsonResponse, Is.Not.Null);
    }
    
    [Test]
    // test if distance and duration are retrieved correctly
    public void Test11()
    {
        RouteService routeService = new RouteService();
        routeService.Configure("5b3ce3597851110001cf624814f69a36c67c4473a3ae8cdd7b8e79e1");
        var jsonResponse = routeService.GetDirAsync("Berlin", "Munich").Result;
        var distanceAndDuration = RouteDecoder.GetDistanceAndDuration(jsonResponse);
        
        // distance in meters
        Assert.That(distanceAndDuration.Item1, Is.EqualTo(585114.625));
        // duration in seconds
        Assert.That(distanceAndDuration.Item2, Is.EqualTo(18974.69921875));
    }
    
    [Test]
    // test other cordinates
    public void Test12()
    {
        RouteService routeService = new RouteService();
        routeService.Configure("5b3ce3597851110001cf624814f69a36c67c4473a3ae8cdd7b8e79e1");
        var coordinates = routeService.GetCoordinatesAsync("Munich").Result;
        
        // lat
        Assert.That(coordinates.Item1, Is.EqualTo("48.152126"));
        // long
        Assert.That(coordinates.Item2, Is.EqualTo("11.544467"));
    }
    
    [Test]
    // test other distance and duration
    public void Test13()
    {
        RouteService routeService = new RouteService();
        routeService.Configure("5b3ce3597851110001cf624814f69a36c67c4473a3ae8cdd7b8e79e1");
        var jsonResponse = routeService.GetDirAsync("Madrid", "Bern").Result;
        var distanceAndDuration = RouteDecoder.GetDistanceAndDuration(jsonResponse);
        
        // distance in meters
        Assert.That(distanceAndDuration.Item1, Is.EqualTo(1522321.25));
        // duration in seconds
        Assert.That(distanceAndDuration.Item2, Is.EqualTo(51808.6015625));
    }
  
    [Test]
    // test tour rating
    public void Test14()
    {
        TourLogs logs = new TourLogs();
        logs.Rating = Rating.Bad;
        
        Assert.That(logs.Rating, Is.EqualTo(Rating.Bad));
    }
    
    [Test]
    // test tour difficulty
    public void Test15()
    {
        TourLogs logs = new TourLogs();
        logs.Difficulty = Difficulty.Hard;
        
        Assert.That(logs.Difficulty, Is.EqualTo(Difficulty.Hard));
    }

    [Test]
    // test if username is set in incorrect format
    public void Test16()
    {
        var username = "name";
        Tour tour = new Tour();
        tour.Name = username;
        bool isValid = tour.HasValidInput();
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    // test drop down menu
    public void Test17()
    {
        Tour tour = new Tour();
        tour.Type = TransportType.Bicycle;
        
        Assert.That(tour.Type, Is.EqualTo(TransportType.Bicycle));
    }
    
    [Test]
    // test drop down menu
    public void Test18()
    {
        Tour tour = new Tour();
        tour.Type = TransportType.Foot;
        
        Assert.That(tour.Type, Is.EqualTo(TransportType.Foot));
    }
    
    [Test]
    // test drop down menu
    public void Test19()
    {
        Tour tour = new Tour();
        tour.Type = TransportType.Car;
        
        Assert.That(tour.Type, Is.EqualTo(TransportType.Car));
    }
    
    [Test]
    // test rating
    public void Test20()
    {
        TourLogs logs = new TourLogs();
        logs.Rating = Rating.Excellent;
        
        Assert.That(logs.Rating, Is.EqualTo(Rating.Excellent));
    }
}