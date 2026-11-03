namespace TMPSLabs.Lab2.Configuration;

public sealed class GameConfig : IGameConfig
{
    private static readonly Lazy<GameConfig> LazyInstance = new(() => new GameConfig());

    private GameConfig()
    {
    }

    public static GameConfig Instance => LazyInstance.Value;

    public Difficulty Difficulty { get; private set; } = Difficulty.Medium;

    public void SetDifficulty(Difficulty difficulty)
    {
        Difficulty = difficulty;
    }
}
