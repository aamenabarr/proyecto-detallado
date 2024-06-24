namespace Fire_Emblem;

public class DenialOfFollowUpDenial : Effect
{
    public DenialOfFollowUpDenial(Unit unit) : base(unit) {}
    
    public override void Apply()
    {
        Unit.DenialOfFollowUpDenial = true;
    }
}