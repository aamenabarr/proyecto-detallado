namespace Fire_Emblem;

public class ExtraDamageInFirstAttack : Effect
{
    private string _stat1 = "";
    private string _stat2 = "";
    private int _percentage = 0;
    private int _percentage2 = 0;
    
    public ExtraDamageInFirstAttack(Unit unit, string stat1, string stat2, int percentage) : base(unit)
    {
        _stat1 = stat1;
        _stat2 = stat2;
        _percentage = percentage;
    }

    public ExtraDamageInFirstAttack(Unit unit, string stat, int percentage = 0) : base(unit, stat)
    {
        _percentage2 = percentage;
    }
    
    public override void Apply()
    {
        AlterDamage(GetExtraDamage());
    }

    private int GetExtraDamage()
    {
        if (_percentage != 0)
            return (Utils.GetUnitStat(Unit, _stat1) - Utils.GetUnitStat(Unit.Rival, _stat2)) * _percentage / 100;
        if (_percentage2 == 0)
            return Utils.GetUnitStat(Unit, Stat);
        return Utils.GetUnitStat(Unit, Stat) * _percentage2 / 100;
    }
}