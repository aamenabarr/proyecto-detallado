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
            !_sympathetic ?
                Math.Round((double)Unit.InitialStats["Hp"] * _value / 100, MidpointRounding.AwayFromZero)
                : Math.Round((double)Unit.InitialStats["Hp"] * _value / 100, MidpointRounding.ToZero)
            : _value;
        return _symbol == ">=" ? Unit.Hp >= comparisonValue : Unit.Hp <= comparisonValue;
    }
}