namespace Fire_Emblem;

public class ExtraDamage : Effect
{
    private int _percentage;
    private bool _inUnit;
    private bool _mastermind;
    private bool _flow;
    public ExtraDamage(Unit unit, int value) : base(unit, value) {}

    public ExtraDamage(
        Unit unit, string stat, int percentage, bool inUnit = false, bool mastermind = false, bool flow = false) 
        : base(unit, stat)
    {
        _percentage = percentage;
        _inUnit = inUnit;
        _mastermind = mastermind;
        _flow = flow;
    }
    
    public override void Apply()
    {
        AlterDamage(Value == 0 ? GetExtraDamage() : Value);
    }

    private int GetExtraDamage()
    {
        if (_mastermind)
        {
            var bonus = Unit.StatsManager.GetEffectValue("Bonus") * _percentage / 100;
            var penalty = Unit.Rival.StatsManager.GetEffectValue("Penalty") * _percentage / 100;
            return (bonus - penalty);
        }
        if (_flow)
            return Math.Min(Math.Max(0, Utils.GetUnitStat(Unit, Stat) - 
                                        Utils.GetUnitStat(Unit.Rival, Stat)) * _percentage / 100, 7);
        if (_inUnit)
            return Utils.GetUnitStat(Unit, Stat) * _percentage / 100;
        return Utils.GetUnitStat(Unit.Rival, Stat) * _percentage / 100;
    }
}