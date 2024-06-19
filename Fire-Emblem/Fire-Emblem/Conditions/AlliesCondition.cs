namespace Fire_Emblem;

public class AlliesCondition : Condition
{
    private Condition _condition;

    public AlliesCondition(Unit unit, Condition condition) : base(unit)
    {
        _condition = condition;
    }
    
    public override bool IsMet()
    {
        for (int i = 1; i <= Unit.Team.Length(); i++)
        {
            _condition.Unit = Unit.Team.GetUnit(i);
            if (_condition.IsMet())
                return true;
        }
        return false;
    }
}