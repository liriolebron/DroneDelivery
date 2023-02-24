using System.Text;
using DroneDelivery.Models;

namespace DroneDelivery.Services;

/// <summary>
/// Represents an efficient drone delivery calculation process
/// </summary>
public class DroneDeliveryService
{
    /// <summary>
    /// Generates the most efficient trip schedule based on drones and deliveries
    /// </summary>
    /// <param name="file">Drone and shipment information</param>
    /// <returns>Formatted delivery program</returns>
    public string ProcessExecution(string file)
    {
        IEnumerable<Drone> drones;
        IEnumerable<Shipment> shipments;

        // Get data from file
        try
        {
            drones = GetDronesFromInput(file);
            shipments = GetShipmentFromInput(file);
        }
        catch (Exception ex)
        {
            return @"Unable to retrieve the data, please make sure the file is available " +
                   $"Error: {ex.Message}";
        }

        // Permute the drone array 
        var dronePermutations = GenerateDronePermutations(drones);

        var droneExecutions = new List<ShipmentExecution>();

        // Calculate best trip number by generating deliveries using drone combinations
        foreach (var dronePermutation in dronePermutations)
        {
            var permutation = dronePermutation as Drone[] ?? dronePermutation.ToArray();
            
            // Assign shipments to execution
            var currentExecution = new ShipmentExecution()
            {
                Shipments = new List<Shipment>(shipments),
                Trips = new List<DroneTrip>()
            };
            
            droneExecutions.Add(currentExecution);

            var iterationCount = 0;

            while (currentExecution.Shipments.Any() || iterationCount == permutation.Count())
            {
                foreach (var drone in permutation.OrderByDescending(d => d.WeightCapacity))
                {
                    var currentDroneCapacity = drone.WeightCapacity;

                    var potentialShipments = currentExecution.Shipments.Where(c => c.Weight <= currentDroneCapacity)
                        .ToArray();

                    // Check if drones can fulfill the deliveries
                    if (!potentialShipments.Any())
                    {
                        iterationCount++;
                        break;
                    }

                    var droneTrip = new DroneTrip()
                    {
                        Drone = drone,
                        Shipments = new List<Shipment>()
                    };

                    currentExecution.Trips.Add(droneTrip);
                    
                    // Fill drone capacity
                    for (var j = 0; j < potentialShipments.Count(); j++)
                    {
                        if (currentDroneCapacity - potentialShipments[j].Weight >= 0)
                        {
                            currentDroneCapacity -= potentialShipments[j].Weight;

                            droneTrip.Shipments.Add(potentialShipments[j]);
                            currentExecution.Shipments.Remove(potentialShipments[j]);
                        }

                        if (currentDroneCapacity == 0)
                        {
                            break;
                        }
                    }
                }
            }

            // Remove execution if it didn't yield fewer trips
            if (iterationCount == permutation.Count() ||
                currentExecution.Trips.Count() > droneExecutions.Min(d => d.Trips.Count()))
            {
                droneExecutions.Remove(currentExecution);
            }
        }

        return GenerateOutput(droneExecutions);
    }

    /// <summary>
    /// Formats and generates the output based on the executions
    /// </summary>
    /// <param name="droneExecutions">Drone executions to format</param>
    /// <returns>Formatted output containing trips by drone</returns>
    private string GenerateOutput(IEnumerable<ShipmentExecution> droneExecutions)
    {
        
        // Group trips by drone
        // Get only the first entry in case
        // Duplicated executions (same amount of trips) 
        // Exist
        var tripsByDrone = droneExecutions
            .FirstOrDefault()
            ?.Trips
            .GroupBy(t => t.Drone)
            .OrderBy(t => t.Key.Name);
        
        var stringBuilder = new StringBuilder();
        
        stringBuilder.AppendLine("Most efficient delivery trips calculation");
        stringBuilder.AppendLine($"Total trips: {tripsByDrone.SelectMany(c => c.ToList()).Count()}");
        stringBuilder.AppendLine("");
        
        foreach (var tripByDrone in tripsByDrone)
        {
            stringBuilder.AppendLine($"{tripByDrone.Key.Name}");

            var tripNumber = 1;
            
            foreach (var trip in tripByDrone.ToList())
            {
                stringBuilder.AppendLine($"Trip #{tripNumber}");
                stringBuilder.AppendLine(string.Join(',', trip.Shipments.Select(c => c.Location.Trim())));
                tripNumber++;
            }
            
            stringBuilder.AppendLine("");
        }
        
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Generates permutations based on the provided drone array
    /// </summary>
    /// <param name="drones">Drones to permute</param>
    /// <returns>Permuted array of drones</returns>
    private List<IEnumerable<Drone>> GenerateDronePermutations(IEnumerable<Drone> drones)
    {
        var dronePermutations = drones.DifferentCombinations(drones.Count() - 1).ToList();
        dronePermutations.Add(drones);
        dronePermutations.Add(drones.OrderByDescending(d => d.WeightCapacity));
        dronePermutations.Add(drones.OrderBy(d => d.WeightCapacity));
        
        return dronePermutations;
    }

    /// <summary>
    /// Gets the drones from the first line of the CSV file
    /// </summary>
    /// <param name="input">CSV file</param>
    /// <returns>Drone array</returns>
    private IEnumerable<Drone> GetDronesFromInput(string input)
    {
        var lines = input.Split('\n');
        var droneLine = lines[0].Split(',');

        for (var i = 0; i < droneLine.Length; i+=2)
        {
            yield return new Drone()
            {
                Name = droneLine[i],
                WeightCapacity = int.Parse(droneLine[i+1])
            };
        }
    }

    /// <summary>
    /// Gets the locations and weight from the CSV file
    /// </summary>
    /// <param name="input">CSV file</param>
    /// <returns>Location array</returns>
    private IEnumerable<Shipment> GetShipmentFromInput(string input)
    {
        var lines = input.Split('\n');
        
        for (var i = 1; i < lines.Length; i++)
        {
            var location = lines[i].Split(',');
            
            yield return new Shipment()
            {
                Location = location[0],
                Weight = int.Parse(location[1])
            };
        }
    }
}