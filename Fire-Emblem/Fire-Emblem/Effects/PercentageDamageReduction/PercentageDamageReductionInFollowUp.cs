namespace Fire_Emblem;

public class PercentageDamageReductionInFollowUp : Effect
{
    private bool _trueDragonWall;

    public PercentageDamageReductionInFollowUp(
        Unit unit, int value, bool trueDragonWall = false) : base(unit, value)
        => _trueDragonWall = trueDragonWall;
    
    public override void Apply()
    {
        if (_trueDragonWall)
            Value = Math.Min(10 * Value,
                Value * (Utils.GetUnitStat(Unit, Stats.Res) - Utils.GetUnitStat(Unit.Rival, Stats.Res)));
        AlterDamage(Value);
    }
}