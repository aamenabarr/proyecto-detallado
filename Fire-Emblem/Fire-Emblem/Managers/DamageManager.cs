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
        "AbsolutDamageReduction",
    };
    private Dictionary<string, int> _damageDictionary = new();
    private View _view;
    private Unit _unit;

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
        _damageDictionary[effect] += value;
    }
    
    public void AlterUnitDamage(string effect)
    {
        if (effect.Contains("PercentageDamageReduction")) _unit.Rival.Dmg += (double)(-_unit.Rival.Damage(_unit) * _damageDictionary[effect]) / 100;
        else if (effect == "AbsolutDamageReduction") _unit.Rival.Dmg += _damageDictionary[effect];
        else _unit.Dmg += _damageDictionary[effect];
    }
    
    public void PrintMessages()
    {
        PrintExtraDamageMessages("ExtraDamage", "cada ataque");
        PrintExtraDamageMessages("ExtraDamageInFirstAttack", "su primer ataque");
        PrintExtraDamageMessages("ExtraDamageInFollowUp", "su Follow-Up");
        PrintPercentageDamageReductionMessages("PercentageDamageReduction", "de los ataques");
        PrintPercentageDamageReductionMessages("PercentageDamageReductionInFirstAttack", "del primer ataque");
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
}