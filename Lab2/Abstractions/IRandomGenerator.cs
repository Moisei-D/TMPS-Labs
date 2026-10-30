namespace TMPSLabs.Lab2.Abstractions;

public interface IRandomGenerator
{
    int Next(int minimumInclusive, int maximumExclusive);
}
