namespace Fire_Emblem;

public class DamageManager
{
    private string[] _effects = {
        "ExtraDamage", 
        "ExtraDamageInFirstAttack", 
        "ExtraDamageInFollowUp", 
        "PercentageDamageReduction", 
        "PercentageDamageReductionInFirstAttack", 
        "PercentageDamageReductionInFollowUp",
        "AbsolutDamageReduction"
    };
    private Unit _unit;
    private double _percentageReduction = 1;
    public Dictionary<string, int> DamageDictionary = new();

    public DamageManager(Unit unit)
    {
        _unit = unit;
        InitializeDamageDictionary();
    }
    
    private void InitializeDamageDictionary()
    {
        foreach (var effect in _effects)
            DamageDictionary.Add(effect, 0);
    }
    
    public void ResetDamageDictionary()
    {
        foreach (var effect in _effects)
            DamageDictionary[effect] = 0;
    }
    
    public void AlterDamageDictionary(string effect, int value)
    {
        if (effect.Contains("PercentageDamageReduction")) 
            AlterPercentageDamageReduction(effect, value / _unit.ReductionOfPercentageDamage);
        else DamageDictionary[effect] += value;
    }

    private void AlterPercentageDamageReduction(string effect, int value)
    {
        if (DamageDictionary[effect] == 0) DamageDictionary[effect] += value;
        else DamageDictionary[effect] = 
            100 - (int)Math.Round((double)(100 - DamageDictionary[effect]) * (100 - value) / 100);
    }
    
    public void AlterUnitDamage(string effect)
    {
        if (effect.Contains("PercentageDamageReduction")) _percentageReduction *= 
            1 - (double)DamageDictionary[effect] / 100;
        else if (effect == "AbsolutDamageReduction") _unit.Rival.Dmg += DamageDictionary[effect];
        else _unit.Dmg += DamageDictionary[effect];
    }

    public void ReduceDamagePercentage(string state)
    {
        _unit.Rival.Dmg += GetPercentageDamageReduction(state, _percentageReduction);
        _percentageReduction = 1;
    }

    private double GetPercentageDamageReduction(string state, double percentageReduction)
    {
        double damageReduction = 
            -(double)(Math.Max(AttackUtils.Damage(_unit.Rival, _unit), 0) + GetRivalExtraDamage(state)) 
            * (1 - percentageReduction);
        damageReduction = Math.Round(damageReduction, 9);
        return Math.Floor(damageReduction);
    }

    private int GetRivalExtraDamage(string state)
    {
        var extraDamage = _unit.Rival.DamageManager.DamageDictionary["ExtraDamage"];
        if (state == "InFirstAttack") extraDamage += 
            _unit.Rival.DamageManager.DamageDictionary["ExtraDamageInFirstAttack"];
        else extraDamage += _unit.Rival.DamageManager.DamageDictionary["ExtraDamageInFollowUp"];
        return extraDamage;
    }

    public int GetDamageReduction()
    {
        var damage = 0;
        _unit.AlterStats();
        _unit.Rival.AlterStats();
        damage += -(int)GetPercentageDamageReduction("InFirstAttack",
            (1 - (double)DamageDictionary["PercentageDamageReductionInFirstAttack"] / 100) *
            (1 - (double)DamageDictionary["PercentageDamageReduction"] / 100));
        _unit.ResetStats();
        _unit.Rival.ResetStats();
        damage += -DamageDictionary["AbsolutDamageReduction"];
        return damage;
    }
    
    public int GetDamage()
    {
        _unit.AlterStats();
        _unit.Rival.AlterStats();
        var damage = AttackUtils.Damage(_unit, _unit.Rival);
        damage += DamageDictionary["ExtraDamage"];
        _unit.ResetStats();
        _unit.Rival.ResetStats();
        return damage;
    }
}