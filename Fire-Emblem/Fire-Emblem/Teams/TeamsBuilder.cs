namespace Fire_Emblem;

public class TeamsBuilder
{
    private Team _team1 = new();
    private Team _team2 = new();
    private Units _units = new();
    
    public Teams PopulateTeams(string[] teamFile)
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
        return new Teams(_team1, _team2);
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
        var unit = new Unit(auxUnit, skills);
        return unit;
    }
}