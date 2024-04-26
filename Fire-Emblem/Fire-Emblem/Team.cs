namespace Fire_Emblem;

public class Team
{
    public List<Unit> Units = new List<Unit>();

    public bool IsValid()
    {
        return HasValidUnits() && HasValidSkills();
    }

    private bool HasValidUnits()
    {
        return !(HasDuplicates(Units, unit => unit.Name) || 
                 Units.Count == 0 || Units.Count > 3);
    }
    
    private bool HasValidSkills()
    {
        return Units.All(unit =>
            !(HasDuplicates(unit.Skills, skill => skill.Name) ||
              unit.Skills.Count > 2));
    }

    private bool HasDuplicates<T>(List<T> collection, Func<T, string> getProperty)
    {
        return collection.Select(getProperty).Distinct().Count() != collection.Count;
    }
}