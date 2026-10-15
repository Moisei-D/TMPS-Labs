using TMPSLabs.Lab1.Abstractions;

namespace TMPSLabs.Lab1.Infrastructure;

public sealed class SystemRandomGenerator : IRandomGenerator
{
    public int Next(int minimumInclusive, int maximumExclusive)
    {
        return Random.Shared.Next(minimumInclusive, maximumExclusive);
    }
}
