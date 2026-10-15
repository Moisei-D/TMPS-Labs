using TMPSLabs.Lab1.Abstractions;
using TMPSLabs.Lab1.Domain.Characters;
using TMPSLabs.Lab1.Domain.World;
using TMPSLabs.Lab1.Services;

namespace TMPSLabs.Lab1.Application;

public sealed class Game
{
    private readonly Player _player;
    private readonly CommandParser _commandParser;
    private readonly CombatService _combatService;
    private readonly IGameInput _input;
    private readonly IGameOutput _output;
    private bool _isRunning = true;

    public Game(
        Player player,
        CommandParser commandParser,
        CombatService combatService,
        IGameInput input,
        IGameOutput output)
    {
        _player = player;
        _commandParser = commandParser;
        _combatService = combatService;
        _input = input;
        _output = output;
    }

    public void Run()
    {
        _output.WriteLine("Dungeon Game - Lab 1", OutputColor.Cyan);
        _output.WriteLine("Type 'help' to see the available commands.");
        DescribeCurrentRoom();

        while (_isRunning && _player.IsAlive)
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
            $"Weapon: {_player.EquippedWeapon.Name}",
            OutputColor.Yellow);
    }

    private void ShowHelp()
    {
        _output.WriteLine(
            "Commands: look, move <direction>, north/east/south/west, " +
            "attack, stats, help, quit");
    }
}
