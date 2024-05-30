using System.Collections;
using Fire_Emblem_View;

namespace Fire_Emblem;

public class StatsManager
{
    private string[] _effects = {
        "AlterBaseStats",
        "Bonus", 
        "BonusInFirstAttack", 
        "BonusInFollowUp", 
        "Penalty", 
        "PenaltyInFirstAttack", 
        "PenaltyInFollowUp"
    };
    private string[] _stats = { 
        "Hp",
        "Atk", 
        "Spd", 
        "Def", 
        "Res"
    };
    private Dictionary<string, Dictionary<string, ArrayList>> _statsDictionary = new();
    private View _view;
    private Unit _unit;

    public StatsManager(View view, Unit unit)
    {
        _view = view;
        _unit = unit;
        InitializeStatsDictionary();
    }

    private void InitializeStatsDictionary()
    {
        foreach (var effect in _effects)
        {
            var dictionary = new Dictionary<string, ArrayList>();
            foreach (var stat in _stats)
            {
                var statInfo = new ArrayList();
                statInfo.Add(0);
                statInfo.Add(false);
                dictionary.Add(stat, statInfo);
            }
            _statsDictionary.Add(effect, dictionary);
        }
    }

    public void AlterStatsDictionary(string effect, string stat, int value)
    {
        var oldValue = (int)_statsDictionary[effect][stat][0];
        _statsDictionary[effect][stat][0] = oldValue + value;
    }
    
    public void NeutralizeEffect(string type, string stat)
    {
        foreach (var effect in _effects)
            if (effect.Contains(type))
            {
                if (stat == null)
                    foreach (var statName in _stats)
                        _statsDictionary[effect][statName][1] = true;
                else
                    _statsDictionary[effect][stat][1] = true;
            }
    }
    
    public void AlterUnitStats(string effect)
    {
        foreach (var (stat, statInfo) in _statsDictionary[effect])
        {
            var value = (int)statInfo[0];
            var neutralized = (bool)statInfo[1];
            if (!neutralized)
               AlterUnitStat(stat, value);
        }
    }
    
    private void AlterUnitStat(string stat, int value)
    {
        switch (stat)
        {
            case "Hp":
                _unit.Hp += value;
                break;
            case "Atk":
                _unit.Atk += value;
                break;
            case "Spd":
                _unit.Spd += value;
                break;
            case "Def":
                _unit.Def += value;
                break;
            case "Res":
                _unit.Res += value;
                break;
        }
    }

    public void PrintMessages()
    {
        PrintAlterStatMessages("Bonus", "+");
        PrintAlterStatMessages("BonusInFirstAttack", "+" , " en su primer ataque");
        PrintAlterStatMessages("BonusInFollowUp", "+", " en su Follow-Up");
        PrintAlterStatMessages("Penalty", "");
        PrintAlterStatMessages("PenaltyInFirstAttack", "", " en su primer ataque");
        PrintAlterStatMessages("PenaltyInFollowUp", "", " en su Follow-Up");
        PrintNeutralizeEffects("Bonus");
        PrintNeutralizeEffects("Penalty");
    }

    private void PrintAlterStatMessages(string effect, string sign, string extraMessage = "")
    {
        foreach (var (stat, statInfo) in _statsDictionary[effect])
        {
            var value = (int)statInfo[0];
            if (value != 0)
                _view.WriteLine($"{_unit.Name} obtiene {stat}{sign}{value}{extraMessage}");
        }
    }

    private void PrintNeutralizeEffects(string effect)
    {
        foreach (var stat in _stats)
            if ((bool)_statsDictionary[effect][stat][1] && stat != "Hp")
                _view.WriteLine($"Los {effect.ToLower()} de {stat} de {_unit.Name} fueron neutralizados");
    }
    
    public void ResetStatsDictionary()
    {
        foreach (var effect in _effects)
        foreach (var stat in _stats)
        {
            _statsDictionary[effect][stat][0] = 0;
            _statsDictionary[effect][stat][1] = false;
        }
    }

    public int Get(string stat)
    {
        var bonusIsNeutralized = (bool)_statsDictionary["Bonus"][stat][1];
        var penaltyIsNeutralized = (bool)_statsDictionary["Penalty"][stat][1];
        
        var bonus = bonusIsNeutralized ? 0 : (int)_statsDictionary["Bonus"][stat][0];
        var penalty = penaltyIsNeutralized ? 0 : (int)_statsDictionary["Penalty"][stat][0];
        
        return bonus + penalty;
    }
}