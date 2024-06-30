namespace Fire_Emblem;

public class HpRange : Condition
{
    private string _symbol;
    private int _value;
    private string _mode;
    private bool _sympathetic;

    public HpRange(Unit unit, string symbol, int value, string mode = "", bool sympathetic = false) : base(unit)
    {
        _symbol = symbol;
        _value = value;
        _mode = mode;
        _sympathetic = sympathetic;
    }
    
    public override bool IsMet()
    {
        var comparisonValue = _mode == "%" ? 
            _sympathetic ?
                GetPercentageValue(MidpointRounding.ToZero)
                : GetPercentageValue(MidpointRounding.AwayFromZero)
            : _value;
        return _symbol == ">=" ? Unit.Hp >= comparisonValue : Unit.Hp <= comparisonValue;
    }

    private double GetPercentageValue(MidpointRounding roundOptions)
    {
        return Math.Round((double)Unit.InitialStats[Stats.Hp] * _value / 100, roundOptions);
    }
}