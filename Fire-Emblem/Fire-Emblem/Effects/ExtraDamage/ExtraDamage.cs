namespace Fire_Emblem;

public class ExtraDamage : Effect
{
    private int _percentage;
    public ExtraDamage(Unit unit, int value) : base(unit, value) {}

    public ExtraDamage(Unit unit, string stat, int percentage) : base(unit, stat)
    {
        _percentage = percentage;
    }
    
    public override void Apply()
    {
        AlterDamage(Value == 0 ? GetExtraDamage() : Value);
    }

    private int GetExtraDamage()
    {
        return Utils.GetUnitStat(Unit.Rival, Stat) * _percentage / 100;
    }
}