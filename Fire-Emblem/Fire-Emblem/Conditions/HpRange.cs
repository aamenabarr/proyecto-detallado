namespace Fire_Emblem;

public class HpRange : Condition
{
    private string _symbol;
    private int _value;
    private string _mode;

    public HpRange(Unit unit, string symbol, int value, string mode = null) : base(unit)
    {
        _symbol = symbol;
        _value = value;
        _mode = mode;
    }
    
    public override bool IsMet()
    {
        if (_mode == "%")
            _value = _unit.InitialHp * _value / 100;
        return _symbol == ">=" ? _unit.Hp >= _value : _unit.Hp <= _value;
    }
}