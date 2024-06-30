namespace Fire_Emblem;

public class AbsolutDamageReduction : Effect
{
    public AbsolutDamageReduction(Unit unit, int value) : base(unit, value) {}
    
    public AbsolutDamageReduction(Unit unit, string stat) : base(unit, stat){}
    
    public override void Apply()
    {
        AlterDamage(Value == 0 ? GetExtraDamage() : Value);
    }

    private int GetExtraDamage()
    {
        return -Math.Min(Math.Max(0, Utils.GetUnitStat(Unit, Stat) - 
                                    Utils.GetUnitStat(Unit.Rival, Stat)) * 70 / 100, 7);
    }
}