namespace Fire_Emblem;

public class StatsComparison : Condition
{
    private int _value1;
    private int _value2;
    private string _symbol;

    public StatsComparison(int value1, string symbol, int value2)
    {
        _value1 = value1; 
        _value2 = value2;
        _symbol = symbol;
    }
    
    public override bool IsMet()
    {
        if (_symbol == ">") return _value1 > _value2;
        return _value1 < _value2;
    }
}