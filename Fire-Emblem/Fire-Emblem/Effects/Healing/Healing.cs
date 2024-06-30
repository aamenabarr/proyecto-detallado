namespace Fire_Emblem;

public class Healing : Effect
{
    public Healing(Unit unit, int value) : base(unit, Stats.Hp, value) {}
    
    public override void Apply()
        => AlterStat();
}