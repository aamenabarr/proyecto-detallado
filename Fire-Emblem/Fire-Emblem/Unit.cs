﻿namespace Fire_Emblem;

public class Unit
{
    public string Name;
    public string Weapon;
    public string Gender;
    public int Hp;
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;
    public int Damage = 0;
    public int FirstAttackerCombat = 0;
    public int FirstDefenderCombat = 0;
    public int FollowUpGuarantee = 0;
    public int DenialOfFollowUp = 0;
    public int ReductionOfPercentageDamage = 1;
    public double Dmg = 0;
    public double Wtb;
    public string[] Skills;
    public bool HasWeaponAdvantage = false;
    public bool IsAttacker = false;
    public bool InFirstCombat = true;
    public bool InFirstAttack = true;
    public bool InFollowUp = false;
    public bool CounterAttackDenial = false;
    public bool DenialOfCounterAttackDenial = false;
    public bool HasAttacked = false;
    public bool DenialOfFollowUpGuarantee = false;
    public bool DenialOfFollowUpDenial = false;
    public Team Team;
    public Unit Rival;
    public Unit LastRival;
    public StatsManager StatsManager;
    public DamageManager DamageManager;
    public Dictionary<string, int> InitialStats;

    public Unit(AuxUnit unit, string[] skills)
    {
        Name = unit.Name;
        Weapon = unit.Weapon;
        Gender = unit.Gender;
        Hp = Utils.Int(unit.HP);
        Atk = Utils.Int(unit.Atk);
        Spd = Utils.Int(unit.Spd);
        Def = Utils.Int(unit.Def);
        Res = Utils.Int(unit.Res);
        Skills = skills;
        StatsManager = new StatsManager(this);
        DamageManager = new DamageManager(this);
        InitialStats = new()
        {
            {Stats.Hp, Hp}, {Stats.Atk, Atk}, {Stats.Spd, Spd}, {Stats.Def, Def}, {Stats.Res, Res}
        };
    }

    public void AlterStats()
    {
        foreach (var effect in SetAlterEffects())
            StatsManager.AlterUnitStats(effect);
    }

    private List<string> SetAlterEffects()
    {
        var effects = new List<string>();
        if (InFirstCombat) effects.Add("AlterBaseStats");
        effects.Add("Bonus");
        effects.Add("Penalty");
        var state= SetState();
        effects.Add($"Bonus{state}");
        effects.Add($"Penalty{state}");
        return effects;
    }

    public void SetTeam(Team team)
        => Team = team;

    private string SetState()
    {
        var state = "";
        if (InFirstAttack) state = "InFirstAttack";
        if (InFollowUp) state = "InFollowUp";
        return state;
    }
    
    public void AlterDamage()
    {
        foreach (var effect in SetDamageEffects())
            DamageManager.AlterUnitDamage(effect);
        DamageManager.ReduceDamagePercentage(SetState());
    }
    
    private List<string> SetDamageEffects()
    {
        var effects = new List<string>();
        effects.Add("ExtraDamage");
        effects.Add("PercentageDamageReduction");
        var state = SetState();
        effects.Add($"ExtraDamage{state}");
        effects.Add($"PercentageDamageReduction{state}");
        effects.Add("AbsolutDamageReduction");
        return effects;
    }
    
    public void ResetStats()
    {
        Atk = InitialStats[Stats.Atk];
        Spd = InitialStats[Stats.Spd];
        Def = InitialStats[Stats.Def];
        Res = InitialStats[Stats.Res];
        Dmg = 0;
    }

    public void ResetManagers()
    {
        StatsManager.ResetStatsDictionary();
        DamageManager.ResetDamageDictionary();
    }

    public void SetFirstCombatInfo()
    {
        if (FirstAttackerCombat == 0) FirstAttackerCombat = IsAttacker ? 1 : 0;
        else FirstAttackerCombat = 2;
        if (FirstDefenderCombat == 0) FirstDefenderCombat = IsAttacker ? 0 : 1;
        else FirstDefenderCombat = 2;
    }

    public void ResetInfo()
    {
        HasAttacked = false;
        CounterAttackDenial = false;
        DenialOfCounterAttackDenial = false;
        FollowUpGuarantee = 0;
        DenialOfFollowUp = 0;
        DenialOfFollowUpGuarantee = false;
        DenialOfFollowUpDenial = false;
        ReductionOfPercentageDamage = 1;
    }
}