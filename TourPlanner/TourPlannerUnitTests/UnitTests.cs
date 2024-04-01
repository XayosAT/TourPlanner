using TourPlanner;
using TourPlanner.Models;

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
    // test if parameters are set correctly
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
    // test if parameters are set correctly
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
}