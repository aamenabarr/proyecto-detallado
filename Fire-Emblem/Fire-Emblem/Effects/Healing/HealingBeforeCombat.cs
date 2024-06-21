namespace Fire_Emblem;

public class HealingBeforeCombat : Effect
{
    private bool _bewitching;

    public HealingBeforeCombat(Unit unit, int value, bool bewitching = false) : base(unit, Stats.Hp, value)
    {
        _bewitching = bewitching;
    }
    
    public override void Apply()
    {
        if (_bewitching) Value = -Utils.GetUnitStat(Unit, Stats.Atk) * Value / 100;
        AlterStat();
    }
}