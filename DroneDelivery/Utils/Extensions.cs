namespace DroneDelivery;

/// <summary>
/// Extension methods
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Permutes the provided array
    /// </summary>
    /// <param name="elements">Array to be permuted</param>
    /// <param name="k">Number of permutations</param>
    /// <typeparam name="T">Extension method base type</typeparam>
    /// <returns>Permuted array</returns>
    public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
            elements.SelectMany((e, i) =>
                elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] {e}).Concat(c)));
    }
}