namespace TMPSLabs.Lab2.Domain.Prototypes;

public sealed class PrototypeRegistry<T> where T : IPrototype<T>
{
    private readonly Dictionary<string, T> _prototypes =
        new(StringComparer.OrdinalIgnoreCase);

    public PrototypeRegistry<T> Register(string key, T prototype)
    {
        _prototypes[key] = prototype;
        return this;
    }

    public T Create(string key)
    {
        if (!_prototypes.TryGetValue(key, out T? prototype))
        {
            throw new KeyNotFoundException($"No prototype registered as '{key}'.");
        }

        return prototype.Clone();
    }
}
