using Fire_Emblem_View;

namespace Fire_Emblem;

public class Game
{
    private string _teamsFolder;
    private View _view;
    private Utils _utils = new Utils();
    private List<AuxUnit> _units;
    private List<AuxSkill> _skills;
    private Battle _battle;
    private Team _team1 = new Team();
    private Team _team2 = new Team();
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        LoadJsonData();
    }

    private void LoadJsonData()
    {
        _units = _utils.LoadFromJsonFile<AuxUnit>("characters.json");
        _skills = _utils.LoadFromJsonFile<AuxSkill>("skills.json");
    }

    public void Play()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        
        var files = _utils.GetFiles(_teamsFolder);
        PrintTeamOptions(files);

        var input = _utils.Int(_view.ReadLine());
        var teamFile = _utils.ReadFile(files[input]);
        PopulateTeams(teamFile);

        if (AreValidTeams())
        {
            _battle = CreateBattle();
            _battle.Start();
        }
        else
            _view.WriteLine($"Archivo de equipos no válido");
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
            (team == 1 ? _team1 : _team2).Units.Add(CreateUnit(unitName, unitSkills));
        }
    }

    private (string, List<Skill>) GetLineInfo(string line)
    {
        var unit = line.Trim(')').Split('(');
        var name = unit[0].Trim();
        var skills = HasSkills(unit)
            ? CreateSkills(unit[1].Split(','))
            : new List<Skill>();
        return (name, skills);
    }

    private bool HasSkills(string[] unit)
    {
        return unit.Length == 2;
    }
    
    private List<Skill> CreateSkills(string[] skills)
    {
        return skills
            .Select(name =>
            {
                var auxSkill = _skills.FirstOrDefault(s => s.Name == name);
                return new Skill(auxSkill);
            })
            .ToList();
    }
    
    private Unit CreateUnit(string name, List<Skill> skills)
    {
        var auxUnit = _units.FirstOrDefault(u => u.Name == name);
        var unit = new Unit(auxUnit, skills, _view);
        SetSkillsUnit(unit);
        return unit;
    }

    private void SetSkillsUnit(Unit unit)
    {
        foreach (var skill in unit.Skills)
            skill.Unit = unit;
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