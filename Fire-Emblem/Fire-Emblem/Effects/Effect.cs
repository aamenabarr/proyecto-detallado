namespace Fire_Emblem;

public class Effect
{
    private string _stat;
    private int _value;
    private Unit _unit;
    
    protected Effect(Unit unit, string stat, int value)
    {
        _unit = unit;
        _stat = stat;
        _value = value;
    }
    
    protected Effect(Unit unit, string stat) : this(unit, stat, 0) {}

    protected Effect(Unit unit, int value) : this(unit, "", value) {}
    
    protected Effect() : this(null, "", 0) {}
    
    public virtual void Apply() {}

    protected void AlterStat()
    {
        _unit.StatsManager.AlterStatsDictionary(GetType().Name, _stat, _value);
    }

    protected void NeutralizeEffect(string type)
    {
        _unit.StatsManager.NeutralizeEffect(type, _stat);
    }

    protected void AlterDamage()
    {
        _unit.DamageManager.AlterDamageDictionary(GetType().Name, _value);
    }
}