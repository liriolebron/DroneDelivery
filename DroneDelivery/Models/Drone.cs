namespace DroneDelivery.Models;

/// <summary>
/// Aircraft without any human pilot, crew, or passengers on board.
/// </summary>
public class Drone
{
    /// <summary>
    /// Drone identifier
    /// </summary>
    public string? Name { get; init; }
    
    /// <summary>
    /// Maximum carry weight capacity
    /// </summary>
    public int WeightCapacity { get; init; }
}