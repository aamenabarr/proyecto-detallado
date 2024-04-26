namespace Fire_Emblem;

public class IsMale : Condition
{
    private Unit _unit;

    public IsMale(Unit unit)
    {
        _unit = unit;
    }
    
    public bool IsMet()
    {
        return _unit.Gender == "Male";
    }
}