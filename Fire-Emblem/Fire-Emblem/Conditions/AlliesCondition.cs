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
        for (int i = 0; i < Unit.Team.Length(); i++)
        {
            _condition.Unit = Unit.Team.GetUnit(i);
            if (_condition.IsMet() && _condition.Unit != Unit && _condition.Unit.Hp > 0)
                return true;
        }
        return false;
    }
}