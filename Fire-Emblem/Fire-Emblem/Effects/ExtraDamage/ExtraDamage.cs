namespace Fire_Emblem;

public class ExtraDamage : Effect
{
    private int _percentage;
    private bool _inUnit;
    public ExtraDamage(Unit unit, int value) : base(unit, value) {}

    public ExtraDamage(Unit unit, string stat, int percentage, bool inUnit = false) : base(unit, stat)
    {
        _percentage = percentage;
        _inUnit = inUnit;
    }
    
    public override void Apply()
    {
        AlterDamage(Value == 0 ? GetExtraDamage() : Value);
    }

    private int GetExtraDamage()
    {
        if (_inUnit)
            return Utils.GetUnitStat(Unit, Stat) * _percentage / 100;
        return Utils.GetUnitStat(Unit.Rival, Stat) * _percentage / 100;
    }
}