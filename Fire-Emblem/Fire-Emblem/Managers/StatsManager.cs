using System.Collections;

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
        "PenaltyInFollowUp",
        "Healing",
        "HealingAfterCombat"
    };
    private Unit _unit;
    public Dictionary<string, Dictionary<string, ArrayList>> StatsDictionary = new();

    public StatsManager(Unit unit)
    {
        _unit = unit;
        InitializeStatsDictionary();
    }

    private void InitializeStatsDictionary()
    {
        foreach (var effect in _effects)
        {
            var dictionary = new Dictionary<string, ArrayList>();
            foreach (var stat in Stats.AllStats)
            {
                var statInfo = new ArrayList();
                statInfo.Add(0);
                statInfo.Add(false);
                dictionary.Add(stat, statInfo);
            }
            StatsDictionary.Add(effect, dictionary);
        }
    }

    public void AlterStatsDictionary(string effect, string stat, int value)
    {
        var oldValue = (int)StatsDictionary[effect][stat][0];
        StatsDictionary[effect][stat][0] = oldValue + value;
    }
    
    public void NeutralizeEffect(string type, string stat)
    {
        foreach (var effect in _effects)
            if (effect.Contains(type))
            {
                if (stat == "")
                    foreach (var statName in Stats.AllStats)
                        StatsDictionary[effect][statName][1] = true;
                else
                    StatsDictionary[effect][stat][1] = true;
            }
    }
    
    public void AlterUnitStats(string effect, bool afterCombat = false)
    {
        foreach (var (stat, statInfo) in StatsDictionary[effect])
        {
            var value = (int)statInfo[0];
            var neutralized = (bool)statInfo[1];
            if (!neutralized || (neutralized && afterCombat && _unit.HasAttacked))
               AlterUnitStat(stat, value, afterCombat);
        }
    }
    
    private void AlterUnitStat(string stat, int value, bool afterCombat)
    {
        switch (stat)
        {
            case "Hp":
                if (afterCombat)
                {
                    if (_unit.Hp > 0 && value != 0)
                        _unit.Hp = Math.Min(_unit.InitialStats["Hp"], _unit.Hp + value);
                }
                else
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
    
    public void ResetStatsDictionary()
    {
        foreach (var effect in _effects)
        foreach (var stat in Stats.AllStats)
        {
            StatsDictionary[effect][stat][0] = 0;
            StatsDictionary[effect][stat][1] = false;
        }
    }

    public int Get(string stat)
    {
        var bonusIsNeutralized = (bool)StatsDictionary["Bonus"][stat][1];
        var penaltyIsNeutralized = (bool)StatsDictionary["Penalty"][stat][1];
        
        var bonus = bonusIsNeutralized ? 0 : (int)StatsDictionary["Bonus"][stat][0];
        var penalty = penaltyIsNeutralized ? 0 : (int)StatsDictionary["Penalty"][stat][0];
        
        return bonus + penalty;
    }
}