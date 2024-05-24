namespace Fire_Emblem;

public abstract class Condition
{
    protected Unit _unit;

    protected Condition(Unit unit)
    {
        _unit = unit;
    }

    protected Condition() : this(null) {}
    
    public abstract bool IsMet();
}