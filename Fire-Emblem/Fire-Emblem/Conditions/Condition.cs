namespace Fire_Emblem;

public abstract class Condition
{
    public Unit Unit;

    protected Condition(Unit unit)
        => Unit = unit;

    protected Condition(){}
    
    public abstract bool IsMet();
}