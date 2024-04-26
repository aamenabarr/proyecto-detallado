namespace Fire_Emblem;

public class HybridOrCondition : Condition
{
    private List<Condition> _conditions;

    public HybridOrCondition(List<Condition> conditions) : base(null)
    {
        _conditions = conditions;
    }
    public override bool IsMet()
    {
        foreach (var condition in _conditions)
            if (condition.IsMet())
                return true;
        return false;
    }
}