using Fire_Emblem_View;
using System.Text.Json;

namespace Fire_Emblem;

public class Game
{
    private readonly View _view;
    private readonly string _teamsFolder;
    private List<AuxUnit>? _units;
    private List<AuxSkill>? _skills;
    private Battle? _battle;
    private readonly Team _team1 = new Team();
    private readonly Team _team2 = new Team();
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        LoadJsonFiles();
    }

    private void LoadJsonFiles()
    {
        _units = LoadFromJsonFile<AuxUnit>("characters.json");
        _skills = LoadFromJsonFile<AuxSkill>("skills.json");
    }

    public void Play()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        
        var files = Directory.GetFiles(_teamsFolder);
        for (var i = 0; i < files.Length; i++)
            _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");

        var input = Convert.ToInt32(_view.ReadLine());
        var teamsFile = ReadTeamsFile(files[input]);
        PopulateTeams(teamsFile);

        if (_team1.IsValid() && _team2.IsValid())
        {
            _battle = new Battle(
                new Player(1, _team1, _view), 
                new Player(2, _team2, _view),
                _view);
            _battle.Start();
        }
        else
            _view.WriteLine($"Archivo de equipos no válido");
    }
    
    private static List<T>? LoadFromJsonFile<T>(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(json);
    }

    private string[] ReadTeamsFile(string teamsFile)
    {
        return File.ReadAllLines(teamsFile);
    }

    private void PopulateTeams(IEnumerable<string> teamsFile)
    {
        var team = 0;
        foreach (var line in teamsFile)
        {
            if (line.StartsWith("Player"))
            {
                team++;
                continue;
            }
            var (unitName, unitSkills) = GetLineInfo(line);
            (team == 1 ? _team1 : _team2).Units.Add(CreateUnit(unitName, unitSkills));
        }
    }

    private (string, List<Skill>) GetLineInfo(string line)
    {
        var unit = line.Trim(')').Split('(');
        var name = unit[0].Trim();
        var skills = unit.Length == 2
            ? CreateSkills(unit[1].Split(','))
            : CreateSkills(["None"]);
        return (name, skills);
    }
    
    private Unit CreateUnit(string name, List<Skill> skills)
    {
        var auxUnit = _units?.FirstOrDefault(u => u.Name == name);
        return new Unit(auxUnit ?? new AuxUnit(), skills, _view);
    }
    
    private List<Skill> CreateSkills(IEnumerable<string> skillNames)
    {
        return skillNames
            .Select(skillName =>
            {
                var auxSkill = _skills?.FirstOrDefault(s => s.Name == skillName);
                return new Skill(auxSkill ?? new AuxSkill(), _view);
            })
            .ToList();
    }
}