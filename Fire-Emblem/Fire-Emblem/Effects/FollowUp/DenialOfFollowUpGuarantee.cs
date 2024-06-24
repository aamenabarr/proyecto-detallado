namespace Fire_Emblem;

public class DenialOfFollowUpGuarantee : Effect
{
    public DenialOfFollowUpGuarantee(Unit unit) : base(unit) {}
    
    public override void Apply()
    {
        Unit.DenialOfFollowUpGuarantee = true;
    }
}