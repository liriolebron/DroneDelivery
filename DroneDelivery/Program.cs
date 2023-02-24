
// See https://aka.ms/new-console-template for more information

using DroneDelivery.Services;

var fileContents = File.ReadAllText(Path.Combine("Data", "Shipments.csv"));
var shipmentExecution = new DroneDeliveryService();

var result = shipmentExecution.ProcessExecution(fileContents);

Console.WriteLine(result);
Console.ReadKey();