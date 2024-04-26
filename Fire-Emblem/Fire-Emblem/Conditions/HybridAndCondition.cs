namespace Fire_Emblem;

public class HybridAndCondition : Condition
{
    private List<Condition> _conditions;

    public HybridAndCondition(List<Condition> conditions)
    {
        _conditions = conditions;
    }
    public bool IsMet()
    {
        foreach (var condition in _conditions)
            if (!condition.IsMet())
                return false;
        return true;
    }
}