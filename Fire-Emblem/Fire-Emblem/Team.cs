namespace Fire_Emblem;

public class Team
{
    private List<Unit> _units = new();

    public bool IsValid()
    {
        return HasValidUnits() && HasValidSkills();
    }

    private bool HasValidUnits()
    {
        return !(HasDuplicatedUnits() || _units.Count == 0 || _units.Count > 3);
    }
    
    private bool HasDuplicatedUnits()
    {
        return _units.Select(unit => unit.Name).Distinct().Count() != _units.Count;
    }
    
    private bool HasValidSkills()
    {
        return _units.All(unit => !(HasDuplicatedSkills(unit.Skills) || unit.Skills.Length > 2));
    }
    
    private bool HasDuplicatedSkills(string[] skills)
    {
        HashSet<string> set = new HashSet<string>();
        foreach (string skill in skills)
            if (!set.Add(skill))
                return true;
        return false;
    }

    public int Length()
    {
        return _units.Count();
    }

    public Unit GetUnit(int id)
    {
        return _units[id];
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }
}