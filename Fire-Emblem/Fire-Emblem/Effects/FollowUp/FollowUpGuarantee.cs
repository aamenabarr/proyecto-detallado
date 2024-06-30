namespace Fire_Emblem;

public class FollowUpGuarantee : Effect
{
    public FollowUpGuarantee(Unit unit) : base(unit, Stats.Hp, 1) {}
    
    public override void Apply()
    {
        AlterStat();
        Unit.FollowUpGuarantee += 1;
    }
}