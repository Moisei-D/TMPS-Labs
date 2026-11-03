namespace TMPSLabs.Lab2.Configuration;

public interface IGameConfig
{
    Difficulty Difficulty { get; }

    void SetDifficulty(Difficulty difficulty);
}
