namespace TMPSLabs.Lab1.Abstractions;

public interface IRandomGenerator
{
    int Next(int minimumInclusive, int maximumExclusive);
}
