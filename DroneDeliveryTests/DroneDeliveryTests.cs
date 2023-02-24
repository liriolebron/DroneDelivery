using DroneDelivery.Services;

namespace DroneDeliveryTests;

public class DroneDeliveryTests
{
    [Test]
    public void Test_Should_Generate_Correct_Output_Six_Trips()
    {
        const string data = @"DroneA,200,DroneB,250,DroneC,100
            LocationA,200
            LocationB,150
            LocationC,50
            LocationD,150
            LocationE,100
            LocationF,200
            LocationG,50
            LocationH,80
            LocationI,70
            LocationJ,50
            LocationK,30
            LocationL,20
            LocationM,50
            LocationN,30
            LocationO,20
            LocationP,90";

        const string expectedOutput = "Most efficient delivery trips calculation\n" +
          "Total trips: 6\n\n" +
          "DroneA\n" +
          "Trip #1\n" +
          "LocationB,LocationG\n" +
          "Trip #2\n" +
          "LocationF\n" +
          "Trip #3\n" +
          "LocationM,LocationN,LocationO,LocationP\n\n" +
          "DroneB\n" +
          "Trip #1\n" +
          "LocationA,LocationC\n" +
          "Trip #2\n" +
          "LocationD,LocationE\n" +
          "Trip #3\n" +
          "LocationH,LocationI,LocationJ,LocationK,LocationL\n\n";

        var service = new DroneDeliveryService();
        var actual =  service.ProcessExecution(data);
        
        Assert.That(actual, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    public void Test_Should_Generate_Correct_Output_Four_Trips()
    {
        const string data = @"DroneA,300,DroneB,350,DroneC,200
            LocationA,200
            LocationB,150
            LocationC,50
            LocationD,150
            LocationE,100
            LocationF,200
            LocationG,50
            LocationH,80
            LocationI,70
            LocationJ,50";

        const string expectedOutput = "Most efficient delivery trips calculation\n" +
          "Total trips: 4\n\n" +
          "DroneA\n" +
          "Trip #1\n" +
          "LocationC,LocationD,LocationE\n" +
          "Trip #2\n" +
          "LocationI,LocationJ\n\n" +
          "DroneB\n" +
          "Trip #1\n" +
          "LocationA,LocationB\n" +
          "Trip #2\n" +
          "LocationF,LocationG,LocationH\n\n";

        var service = new DroneDeliveryService();
        var actual =  service.ProcessExecution(data);
        
        Assert.That(actual, Is.EqualTo(expectedOutput));
    }
}