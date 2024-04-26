namespace Fire_Emblem;

public abstract class Condition
{
    protected Unit _unit;

    protected Condition(Unit unit)
    {
        _unit = unit;
    }
    
    public abstract bool IsMet();
}