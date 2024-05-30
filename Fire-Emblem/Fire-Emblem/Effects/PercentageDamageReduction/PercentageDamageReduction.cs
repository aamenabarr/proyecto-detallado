namespace Fire_Emblem;

public class PercentageDamageReduction : Effect
{
    public PercentageDamageReduction(Unit unit, int value) : base(unit, value) {}
    
    public PercentageDamageReduction(Unit unit, string stat) : base(unit, stat) {}
    
    public override void Apply()
    {
        AlterDamage(Value == 0 ? GetReductionPercentage() : Value);
    }

    private int GetReductionPercentage()
    {
        return Math.Min((Utils.GetUnitStat(Unit, Stat) - Utils.GetUnitStat(Unit.Rival, Stat)) * 4, 40);
    }
}