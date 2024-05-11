namespace Fire_Emblem;

public class NeutralizePenalty : Effect
{
    public NeutralizePenalty(Unit unit, string stat) : base(unit, stat) {}
    
    public NeutralizePenalty(Unit unit) : this(unit, null) {}
    
    public override void Apply()
    {
        NeutralizeEffect("Penalty");
    }
}