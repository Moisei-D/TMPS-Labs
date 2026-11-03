namespace TMPSLabs.Lab2.Domain.Prototypes;

public interface IPrototype<out T>
{
    T Clone();
}
