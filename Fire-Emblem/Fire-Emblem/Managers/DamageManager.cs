using Fire_Emblem_View;

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
    private Dictionary<string, int> _damageDictionary = new();
    private View _view;
    private Unit _unit;
    private double _percentageReduction = 1;

    public DamageManager(View view, Unit unit)
    {
        _view = view;
        _unit = unit;
        InitializeDamageDictionary();
    }
    
    private void InitializeDamageDictionary()
    {
        foreach (var effect in _effects)
            _damageDictionary.Add(effect, 0);
    }
    
    public void AlterDamageDictionary(string effect, int value)
    {
        if (effect.Contains("PercentageDamageReduction")) AlterPercentageDamageReduction(effect, value);
        else _damageDictionary[effect] += value;
    }

    private void AlterPercentageDamageReduction(string effect, int value)
    {
        if (_damageDictionary[effect] == 0) _damageDictionary[effect] += value;
        else _damageDictionary[effect] = 
            100 - (int)Math.Round((double)(100 - _damageDictionary[effect]) * (100 - value) / 100);
    }
    
    public void AlterUnitDamage(string effect)
    {
        if (effect.Contains("PercentageDamageReduction")) _percentageReduction *= 
            1 - (double)_damageDictionary[effect] / 100;
        else if (effect == "AbsolutDamageReduction") _unit.Rival.Dmg += _damageDictionary[effect];
        else _unit.Dmg += _damageDictionary[effect];
    }

    public void ReduceDamagePercentage(string state)
    {
        _unit.Rival.Dmg += GetPercentageDamageReduction(state, _percentageReduction);
        _percentageReduction = 1;
    }

    private double GetPercentageDamageReduction(string state, double percentageReduction)
    {
        double damageReduction = 
            -(double)(Math.Max(_unit.Rival.Damage(_unit), 0) + GetRivalExtraDamage(state)) 
            * (1 - percentageReduction);
        damageReduction = Math.Round(damageReduction, 9);
        return Math.Floor(damageReduction);
    }

    private int GetRivalExtraDamage(string state)
    {
        var extraDamage = _unit.Rival.DamageManager._damageDictionary["ExtraDamage"];
        if (state == "InFirstAttack") extraDamage += 
            _unit.Rival.DamageManager._damageDictionary["ExtraDamageInFirstAttack"];
        else extraDamage += _unit.Rival.DamageManager._damageDictionary["ExtraDamageInFollowUp"];
        return extraDamage;
    }
    
    public void PrintMessages()
    {
        PrintExtraDamageMessages("ExtraDamage", "cada ataque");
        PrintExtraDamageMessages("ExtraDamageInFirstAttack", "su primer ataque");
        PrintExtraDamageMessages("ExtraDamageInFollowUp", "su Follow-Up");
        PrintPercentageDamageReductionMessages("PercentageDamageReduction", "de los ataques");
        PrintPercentageDamageReductionMessages(
            "PercentageDamageReductionInFirstAttack", "del primer ataque");
        PrintPercentageDamageReductionMessages("PercentageDamageReductionInFollowUp", "del Follow-Up");
        PrintAbsolutDamageReductionMessages();
    }

    private void PrintExtraDamageMessages(string effect, string extraMessage)
    {
        var value = _damageDictionary[effect];
        if (value != 0)
            _view.WriteLine($"{_unit.Name} realizará +{value} daño extra en {extraMessage}");
    }
    
    private void PrintPercentageDamageReductionMessages(string effect, string extraMessage)
    {
        var value = _damageDictionary[effect];
        if (value != 0)
            _view.WriteLine($"{_unit.Name} reducirá el daño {extraMessage} del rival en un {value}%");
    }
    
    private void PrintAbsolutDamageReductionMessages()
    {
        var value = _damageDictionary["AbsolutDamageReduction"];
        if (value != 0)
            _view.WriteLine($"{_unit.Name} recibirá {value} daño en cada ataque");
    }
    
    public void ResetDamageDictionary()
    {
        foreach (var effect in _effects)
            _damageDictionary[effect] = 0;
    }

    public int GetDamageReduction()
    {
        var damage = 0;
        _unit.AlterStats();
        _unit.Rival.AlterStats();
        damage += -(int)GetPercentageDamageReduction("InFirstAttack",
            (1 - (double)_damageDictionary["PercentageDamageReductionInFirstAttack"] / 100) *
            (1 - (double)_damageDictionary["PercentageDamageReduction"] / 100));
        _unit.ResetStats();
        _unit.Rival.ResetStats();
        damage += -_damageDictionary["AbsolutDamageReduction"];
        return damage;
    }
}