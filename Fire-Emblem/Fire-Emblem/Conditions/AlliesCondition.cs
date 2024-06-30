namespace Fire_Emblem;

public class AlliesCondition : Condition
{
    private Condition _condition;

    public AlliesCondition(Unit unit, Condition condition) : base(unit)
        => _condition = condition;
    
    public override bool IsMet()
    {
        for (int i = 0; i < Unit.Team.Length(); i++)
        {
            _condition.Unit = Unit.Team.GetUnit(i);
            if (UnitIsAlly(_condition.Unit) && AllyIsAlive(_condition.Unit) && _condition.IsMet())
                return true;
        }
        return false;
    }

    private bool UnitIsAlly(Unit possibleAlly)
    {
        return possibleAlly != Unit;
    }

    private bool AllyIsAlive(Unit unit)
    {
        return unit.Hp > 0;
    }
}