namespace Fire_Emblem;

public class StatsComparison : Condition
{
    private Unit _unit1;
    private Unit _unit2;
    private string _stat1;
    private string _stat2 = "";
    private string _symbol;
    private int _extraValue;
    private bool _flight;

    public StatsComparison(Unit unit1, string stat1, string symbol, Unit unit2, string stat2, int extraValue = 0, bool flight = false)
    {
        _unit1 = unit1; 
        _unit2 = unit2;
        _stat1 = stat1; 
        _stat2 = stat2;
        _symbol = symbol;
        _extraValue = extraValue;
        _flight = flight;
    }

    public StatsComparison(Unit unit, string stat)
    {
        _unit1 = unit;
        _stat1 = stat;
    }
    
    public override bool IsMet()
    {
        if (_flight) return _unit1.InitialStats[_stat1] >= _unit2.InitialStats[_stat2] + _extraValue;
        if (_stat2 == "")
        {
            var statsUnit1 = Utils.GetUnitStat(_unit1, Stats.Spd) + Utils.GetUnitStat(_unit1, _stat1);
            var statsUnit2 = Utils.GetUnitStat(_unit1.Rival, Stats.Spd) + Utils.GetUnitStat(_unit1.Rival, _stat1);
            return statsUnit1 > statsUnit2;
        }
        var value1 = Utils.GetUnitStat(_unit1, _stat1);
        var value2 = Utils.GetUnitStat(_unit2, _stat2) + _extraValue;
        if (_symbol == ">") 
            return value1 > value2;
        if (_symbol == ">=")
            return value1 >= value2;
        return value1 < value2;
    }
}