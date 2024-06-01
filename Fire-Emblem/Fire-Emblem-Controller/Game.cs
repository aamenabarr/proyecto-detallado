using Fire_Emblem_View;
using Fire_Emblem;

namespace Fire_Emblem_Controller;

public class Game
{
    private string _teamsFolder;
    private View _view;
    private Battle _battle;
    private Units _units = new();
    private Team _team1 = new();
    private Team _team2 = new();
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        var teamFile = ChooseTeamFile();
        PopulateTeams(teamFile);

        if (AreValidTeams())
        {
            _battle = CreateBattle();
            _battle.Start();
        }
        else
            _view.WriteLine($"Archivo de equipos no válido");
    }

    private string[] ChooseTeamFile()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        
        var files = Utils.GetFiles(_teamsFolder);
        PrintTeamOptions(files);

        var input = Utils.Int(_view.ReadLine());
        return Utils.ReadFile(files[input]);
    }

    private void PrintTeamOptions(string[] files)
    {
        for (var i = 0; i < files.Length; i++)
            _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
    }

    private void PopulateTeams(string[] teamFile)
    {
        var team = 0;
        foreach (var line in teamFile)
        {
            if (line.StartsWith("Player"))
            {
                team++;
                continue;
            }
            var (unitName, unitSkills) = GetLineInfo(line);
            (team == 1 ? _team1 : _team2).AddUnit(CreateUnit(unitName, unitSkills));
        }
    }

    private (string, string[]) GetLineInfo(string line)
    {
        var unit = line.Trim(')').Split('(');
        var name = unit[0].Trim();
        var skills = HasSkills(unit)
            ? unit[1].Split(',')
            : new string[1];
        return (name, skills);
    }
    
    private bool HasSkills(string[] unit)
    {
        return unit.Length == 2;
    }
    
    private Unit CreateUnit(string name, string[] skills)
    {
        var auxUnit = _units.GetUnit(name);
        var unit = new Unit(auxUnit, skills, _view);
        return unit;
    }

    private bool AreValidTeams()
    {
        return _team1.IsValid() && _team2.IsValid();
    }

    private Battle CreateBattle()
    {
        return new Battle(
            new Player(1, _team1), 
            new Player(2, _team2),
            _view);
    }
}