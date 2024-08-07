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
        var lengthNoDuplicatedUnits = _units.Select(unit => unit.Name).Distinct().Count();
        return lengthNoDuplicatedUnits != _units.Count;
    }
    
    private bool HasValidSkills()
    {
        return _units.All(unit => 
            !HasDuplicatedSkills(unit.Skills) && unit.Skills.Length <= 2);
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
        var unit = _units[id];
        unit.SetTeam(this);
        return unit;
    }

    public void RemoveUnit(Unit unit)
        => _units.Remove(unit);

    public void AddUnit(Unit unit)
        => _units.Add(unit);
}