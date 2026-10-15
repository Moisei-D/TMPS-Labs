namespace TMPSLabs.Lab1.Abstractions;

public enum OutputColor
{
    Default,
    Red,
    Green,
    Yellow,
    Blue,
    Cyan,
    DarkGray
}

public interface IGameOutput
{
    void Write(string message, OutputColor color = OutputColor.Default);
    void WriteLine(string message = "", OutputColor color = OutputColor.Default);
}
