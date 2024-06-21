namespace Fire_Emblem;

public class ExtraDamage : Effect
{
    private int _percentage;
    private bool _inUnit;
    private bool _mastermind;
    public ExtraDamage(Unit unit, int value) : base(unit, value) {}

    public ExtraDamage(Unit unit, string stat, int percentage, bool inUnit = false, bool mastermind = false) : base(unit, stat)
    {
        _percentage = percentage;
        _inUnit = inUnit;
        _mastermind = mastermind;
    }
    
    public override void Apply()
    {
        AlterDamage(Value == 0 ? GetExtraDamage() : Value);
    }

    private int GetExtraDamage()
    {
        if (_mastermind)
        {
            var bonus = Unit.StatsManager.GetEffectValue("Bonus");
            var penalty = Unit.Rival.StatsManager.GetEffectValue("Penalty");
            return (bonus - penalty) * 80 / 100;
        }
        if (_inUnit)
            return Utils.GetUnitStat(Unit, Stat) * _percentage / 100;
        return Utils.GetUnitStat(Unit.Rival, Stat) * _percentage / 100;
    }
}