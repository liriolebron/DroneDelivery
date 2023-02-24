namespace DroneDelivery.Models;

/// <summary>
/// Represents a delivery trip performed by a drone
/// </summary>
public class DroneTrip
{
    /// <summary>
    /// Drone
    /// </summary>
    public Drone? Drone { get; init; }
    
    /// <summary>
    /// Shipments on the trip
    /// </summary>
    public List<Shipment>? Shipments { get; init; }
}