using TMPSLabs.Lab2.Abstractions;
using TMPSLabs.Lab2.Configuration;
using TMPSLabs.Lab2.Domain.Characters;
using TMPSLabs.Lab2.Domain.Items;
using TMPSLabs.Lab2.Domain.World;
using TMPSLabs.Lab2.Services;

namespace TMPSLabs.Lab2.Application;

public sealed class Game
{
    private readonly Player _player;
    private readonly CommandParser _commandParser;
    private readonly CombatService _combatService;
    private readonly MapRenderer _mapRenderer;
    private readonly IGameConfig _config;
    private readonly IGameInput _input;
    private readonly IGameOutput _output;
    private bool _isRunning = true;
    private bool _hasWon;

    public Game(
        Player player,
        CommandParser commandParser,
        CombatService combatService,
        MapRenderer mapRenderer,
        IGameConfig config,
        IGameInput input,
        IGameOutput output)
    {
        _player = player;
        _commandParser = commandParser;
        _combatService = combatService;
        _mapRenderer = mapRenderer;
        _config = config;
        _input = input;
        _output = output;
    }

    public void Run()
    {
        _output.WriteLine("Dungeon Game - Lab 2", OutputColor.Cyan);
        ChooseDifficulty();
        _output.WriteLine("Type 'help' to see the available commands.");
        DescribeCurrentRoom();

        while (_isRunning && _player.IsAlive && !_hasWon)
        {
            _output.Write("\n> ", OutputColor.Blue);
            string? input = _input.ReadLine();

            if (input is null)
            {
                _isRunning = false;
                continue;
            }

            Execute(_commandParser.Parse(input));
        }

        if (!_player.IsAlive)
        {
            _output.WriteLine("You have fallen in the dungeon.");
        }
        else if (_hasWon)
        {
            _output.WriteLine("The drake collapses. You conquered the dungeon!", OutputColor.Green);
        }

        _output.WriteLine("Game over.");
    }

    private void Execute(GameCommand command)
    {
        switch (command.Type)
        {
            case CommandType.Look:
                DescribeCurrentRoom();
                break;
            case CommandType.Move:
                Move(command.Argument);
                break;
            case CommandType.Attack:
                Attack();
                break;
            case CommandType.Stats:
                ShowStats();
                break;
            case CommandType.Take:
                Take();
                break;
            case CommandType.Equip:
                Equip(command.Argument);
                break;
            case CommandType.Help:
                ShowHelp();
                break;
            case CommandType.Quit:
                _isRunning = false;
                break;
            default:
                _output.WriteLine(
                    "Unknown command. Type 'help' for assistance.",
                    OutputColor.Red);
                break;
        }
    }

    private void Move(string? directionText)
    {
        if (!Enum.TryParse(directionText, ignoreCase: true, out Direction direction))
        {
            _output.WriteLine(
                "Choose north, east, south or west.",
                OutputColor.Red);
            return;
        }

        if (_player.CurrentRoom.Enemy?.IsAlive == true)
        {
            _output.WriteLine(
                "You cannot leave while an enemy blocks the way.",
                OutputColor.Red);
            return;
        }

        if (!_player.CurrentRoom.TryGetExit(direction, out Room? destination) ||
            destination is null)
        {
            _output.WriteLine(
                "There is no passage in that direction.",
                OutputColor.Red);
            return;
        }

        _player.MoveTo(destination);
        DescribeCurrentRoom();
    }

    private void Attack()
    {
        Enemy? enemy = _player.CurrentRoom.Enemy;

        if (enemy is null || !enemy.IsAlive)
        {
            _output.WriteLine("There is nothing here to attack.");
            return;
        }

        _combatService.ResolveRound(_player, enemy);

        if (enemy is { IsBoss: true, IsAlive: false })
        {
            _hasWon = true;
        }
    }

    private void ChooseDifficulty()
    {
        while (true)
        {
            _output.Write("Choose difficulty (easy, medium, hard): ", OutputColor.Blue);
            string? input = _input.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                _config.SetDifficulty(Difficulty.Medium);
                break;
            }

            if (Enum.TryParse(input.Trim(), ignoreCase: true, out Difficulty difficulty))
            {
                _config.SetDifficulty(difficulty);
                break;
            }

            _output.WriteLine("Unknown difficulty.", OutputColor.Red);
        }

        _output.WriteLine($"Difficulty: {_config.Difficulty}", OutputColor.Yellow);
    }

    private void Take()
    {
        IWeapon? weapon = _player.CurrentRoom.TakeLoot();

        if (weapon is null)
        {
            _output.WriteLine("There is nothing here to take.");
            return;
        }

        _player.AddWeapon(weapon);
        _output.WriteLine(
            $"You pick up the {weapon.Name} ({weapon.MinimumDamage}-{weapon.MaximumDamage} damage). " +
            "Use 'equip <weapon>' to wield it.",
            OutputColor.Yellow);
    }

    private void Equip(string? weaponName)
    {
        if (string.IsNullOrWhiteSpace(weaponName) || !_player.TryEquip(weaponName))
        {
            _output.WriteLine(
                "You do not carry that weapon.",
                OutputColor.Red);
            return;
        }

        _output.WriteLine($"You wield the {_player.EquippedWeapon.Name}.", OutputColor.Yellow);
    }

    private void DescribeCurrentRoom()
    {
        Room room = _player.CurrentRoom;
        _output.WriteLine($"\n{room.Name}", OutputColor.Cyan);
        _output.WriteLine(room.Description);

        if (room.Enemy?.IsAlive == true)
        {
            _output.WriteLine(
                $"A {room.Enemy.Name} blocks your path ({room.Enemy.Health} HP).",
                OutputColor.Red);
        }

        if (room.Loot is not null)
        {
            _output.WriteLine($"Loot on the floor: {room.Loot.Name}.", OutputColor.Yellow);
        }

        foreach (string line in _mapRenderer.Render(_player))
        {
            _output.WriteLine(line, OutputColor.Cyan);
        }

        string exits = string.Join(
            ", ",
            room.AvailableDirections.Select(
                direction => direction.ToString().ToLowerInvariant()));

        _output.WriteLine($"Exits: {exits}", OutputColor.DarkGray);
    }

    private void ShowStats()
    {
        _output.WriteLine(
            $"{_player.Name}: {_player.Health}/{_player.MaximumHealth} HP",
            OutputColor.Green);
        _output.WriteLine(
            "Weapons: " + string.Join(", ", _player.Inventory.Select(
                weapon => weapon == _player.EquippedWeapon
                    ? $"[{weapon.Name}]"
                    : weapon.Name)),
            OutputColor.Yellow);
    }

    private void ShowHelp()
    {
        _output.WriteLine(
            "Commands: look, move <direction>, north/east/south/west, " +
            "attack, take, equip <weapon>, stats, help, quit");
    }
}
