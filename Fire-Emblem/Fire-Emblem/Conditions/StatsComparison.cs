namespace Fire_Emblem;

public class StatsComparison : Condition
{
    private Unit _unit1;
    private Unit _unit2;
    private string _stat1;
    private string _stat2;
    private string _symbol;
    private int _extraValue;

    public StatsComparison(Unit unit1, string stat1, string symbol, Unit unit2, string stat2, int extraValue = 0)
    {
        _unit1 = unit1; 
        _unit2 = unit2;
        _stat1 = stat1; 
        _stat2 = stat2;
        _symbol = symbol;
        _extraValue = extraValue;
    }
    
    public override bool IsMet()
    {
        var value1 = Utils.GetUnitStat(_unit1, _stat1);
        var value2 = Utils.GetUnitStat(_unit2, _stat2) + _extraValue;
        if (_symbol == ">") 
            return value1 > value2;
        if (_symbol == ">=")
            return value1 >= value2;
        return value1 < value2;
    }
}