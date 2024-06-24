namespace Fire_Emblem;

public class DenialOfFollowUp : Effect
{
    public DenialOfFollowUp(Unit unit) : base(unit, Stats.Hp, 1) {}
    
    public override void Apply()
    {
        AlterStat();
        Unit.DenialOfFollowUp = true;
    }
}