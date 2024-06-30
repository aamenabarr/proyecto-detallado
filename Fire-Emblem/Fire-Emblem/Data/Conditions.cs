namespace Fire_Emblem;

public class Conditions
{
    private List<Condition> _conditions = new();

    public void Add(Condition condition)
        => _conditions.Add(condition);
    
    
    public List<Condition> Get()
    {
        return _conditions;
    }
}