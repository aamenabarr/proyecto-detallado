namespace Fire_Emblem;

public class NeutralizeBonus : Effect
{
    public NeutralizeBonus(Unit unit, string stat) : base(unit, stat) {}
    
    public NeutralizeBonus(Unit unit) : this(unit, "") {}

    public override void Apply()
    {
        NeutralizeEffect("Bonus");
    }
}