namespace Fire_Emblem;

public class LastRival : Condition
{
    private Unit _unit;

    public LastRival(Unit unit)
    {
        _unit = unit;
    }
    
    public bool IsMet()
    {
        return _unit.LastRival == _unit.Rival;
    }
}