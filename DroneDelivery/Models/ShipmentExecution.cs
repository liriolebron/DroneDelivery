namespace DroneDelivery.Models;

/// <summary>
/// Represents a shipment execution containing the shipment lists
/// and each trips performed by a given drone combination
/// </summary>
public class ShipmentExecution
{
    /// <summary>
    /// Shipments on the execution
    /// </summary>
    public List<Shipment>? Shipments { get; set; }
    
    /// <summary>
    /// Total trips performed
    /// </summary>
    public List<DroneTrip>? Trips { get; set; }
}