namespace Fire_Emblem;

public class HpRange : Condition
{
    private Unit _unit;
    private string _symbol;
    private int _value;
    private string _mode;

    public HpRange(Unit unit, string symbol, int value, string mode = null)
    {
        _unit = unit;
        _symbol = symbol;
        _value = value;
        _mode = mode;
    }
    
    public bool IsMet()
    {
        if (_mode == "%")
            _value = _unit.InitialHp * _value / 100;
        return _symbol == ">=" ? _unit.Hp >= _value : _unit.Hp <= _value;
    }
}