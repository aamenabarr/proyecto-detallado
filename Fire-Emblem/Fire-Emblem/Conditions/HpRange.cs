namespace Fire_Emblem;

public class HpRange : Condition
{
    private string _symbol;
    private int _value;
    private string _mode;

    public HpRange(Unit unit, string symbol, int value, string mode = "") : base(unit)
    {
        _symbol = symbol;
        _value = value;
        _mode = mode;
    }
    
    public override bool IsMet()
    {
        var comparisonValue = _mode == "%" ? 
            Math.Round((double)Unit.InitialStats["Hp"] * _value / 100, MidpointRounding.AwayFromZero) 
            : _value;
        return _symbol == ">=" ? Unit.Hp >= comparisonValue : Unit.Hp <= comparisonValue;
    }
}