namespace Fire_Emblem;

public class ExtraDamageInFollowUp : Effect
{
    public ExtraDamageInFollowUp(Unit unit, string stat) : base(unit, stat) {}
    
    public override void Apply()
    {
        AlterDamage(GetExtraDamage());
    }

    private int GetExtraDamage()
    {
        return Utils.GetUnitStat(Unit, Stat);
    }
}