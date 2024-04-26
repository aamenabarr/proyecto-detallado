namespace Fire_Emblem;

public class StartsAttack : Condition
{
    private Unit _unit;

    public StartsAttack(Unit unit)
    {
        _unit = unit;
    }
    
    public bool IsMet()
    {
        return _unit.IsAttacker;
    }
}