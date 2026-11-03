using TMPSLabs.Lab2.Abstractions;
using TMPSLabs.Lab2.Configuration;
using TMPSLabs.Lab2.Domain.Characters;

namespace TMPSLabs.Lab2.Services;

public sealed class CombatService
{
    private readonly IRandomGenerator _random;
    private readonly IGameOutput _output;
    private readonly IGameConfig _config;

    public CombatService(IRandomGenerator random, IGameOutput output, IGameConfig config)
    {
        _random = random;
        _output = output;
        _config = config;
    }

    public void ResolveRound(Player player, Enemy enemy)
    {
        int playerDamage = RollPlayerDamage(
            player.EquippedWeapon.MinimumDamage,
            player.EquippedWeapon.MaximumDamage);
        enemy.TakeDamage(playerDamage);

        _output.WriteLine(
            $"You strike the {enemy.Name} with your {player.EquippedWeapon.Name} " +
            $"for {playerDamage} damage.",
            OutputColor.Yellow);

        if (!enemy.IsAlive)
        {
            _output.WriteLine($"The {enemy.Name} is defeated.", OutputColor.Green);

            if (enemy.Drop is not null)
            {
                player.CurrentRoom.PlaceLoot(enemy.Drop);
                _output.WriteLine(
                    $"It dropped: {enemy.Drop.Name}. Type 'take' to pick it up.",
                    OutputColor.Yellow);
            }

            return;
        }

        int enemyDamage = RollEnemyDamage(enemy.MinimumDamage, enemy.MaximumDamage);

        player.TakeDamage(enemyDamage);
        _output.WriteLine(
            $"The {enemy.Name} hits you for {enemyDamage} damage.",
            OutputColor.Red);
    }

    private int RollPlayerDamage(int minimum, int maximum)
    {
        RollBias bias = _config.Difficulty switch
        {
            Difficulty.Easy => RollBias.Higher,
            Difficulty.Hard => RollBias.Lower,
            _ => RollBias.None
        };

        return Roll(minimum, maximum, bias);
    }

    private int RollEnemyDamage(int minimum, int maximum)
    {
        RollBias bias = _config.Difficulty switch
        {
            Difficulty.Easy => RollBias.Lower,
            Difficulty.Hard => RollBias.Higher,
            _ => RollBias.None
        };

        return Roll(minimum, maximum, bias);
    }

    // A biased roll takes two uniform rolls and keeps the lower or higher one,
    // which makes the edge of the range noticeably more likely.
    private int Roll(int minimum, int maximum, RollBias bias)
    {
        int first = _random.Next(minimum, maximum + 1);

        if (bias == RollBias.None)
        {
            return first;
        }

        int second = _random.Next(minimum, maximum + 1);
        return bias == RollBias.Lower
            ? Math.Min(first, second)
            : Math.Max(first, second);
    }

    private enum RollBias
    {
        None,
        Lower,
        Higher
    }
}
