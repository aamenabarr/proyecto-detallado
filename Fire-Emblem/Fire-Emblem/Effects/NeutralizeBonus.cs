namespace Fire_Emblem;

public class NeutralizeBonus : Effect
{
    public NeutralizeBonus(Unit unit, string stat) : base(unit, stat) {}
    
    public NeutralizeBonus(Unit unit) : this(unit, null) {}

    public override void Apply()
    {
        base.Apply();
        Neutralize(GetEffects<Bonus>());
        AddMessage("bonus");
    }
    
    public void AddMessage(string type)
    {
        AddNeutralizeMessages(type);
    }
}