using TMPSLabs.Lab2.Abstractions;

namespace TMPSLabs.Lab2.Infrastructure;

public sealed class SystemRandomGenerator : IRandomGenerator
{
    public int Next(int minimumInclusive, int maximumExclusive)
    {
        return Random.Shared.Next(minimumInclusive, maximumExclusive);
    }
}
