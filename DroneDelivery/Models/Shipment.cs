namespace DroneDelivery.Models;

/// <summary>
/// Represents a package shipment
/// </summary>
public class Shipment
{
    /// <summary>
    /// Location name of the shipment
    /// </summary>
    public string? Location { get; init; }
    
    /// <summary>
    /// Weight of the shipment
    /// </summary>
    public int Weight { get; init; }
}