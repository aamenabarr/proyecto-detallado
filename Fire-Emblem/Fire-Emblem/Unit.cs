using Fire_Emblem_View;

namespace Fire_Emblem;

public class Unit
{
    public string Name;
    public string Weapon;
    public string Gender;
    public string DeathQuote;
    public int Hp;
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;
    public double Wtb;
    public string[] Skills;
    public bool IsAttacker = false;
    public bool InFirstRound = true;
    public bool InFirstAttack = true;
    public bool InFollowUp = false;
    public Unit Rival;
    public Unit LastRival;
    public StatsManager StatsManager;
    public Dictionary<string, int> InitialStats;
    private View _view;

    public Unit(AuxUnit unit, string[] skills, View view)
    {
        Name = unit.Name;
        Weapon = unit.Weapon;
        Gender = unit.Gender;
        DeathQuote = unit.DeathQuote;
        Hp = Utils.Int(unit.HP);
        Atk = Utils.Int(unit.Atk);
        Spd = Utils.Int(unit.Spd);
        Def = Utils.Int(unit.Def);
        Res = Utils.Int(unit.Res);
        Skills = skills;
        StatsManager = new StatsManager(view, this);
        InitialStats = new()
        {
            {"Hp", Hp}, {"Atk", Atk}, {"Spd", Spd}, {"Def", Def}, {"Res", Res}
        };
        _view = view;
    }
    
    public void Attack(Unit rival)
    {
        var damage = Math.Max(0, Damage(rival));
        _view.WriteLine($"{Name} ataca a {rival.Name} con {damage} de daño");
        rival.ReduceHp(damage);
    }

    private int Defense(Unit rival)
    {
        return rival.Weapon == "Magic" ? Res : Def;
    }

    private int Damage(Unit rival)
    {
        var attack = (int)Math.Floor(Atk * Wtb);
        return (attack - rival.Defense(this));
    }

    private void ReduceHp(int damage)
    {
        Hp = Math.Max(0, Hp - damage);
    }

    public bool CanDoFollowUp(Unit rival)
    {
        var rivalSpeed = (rival.Spd);
        return (Spd - rivalSpeed) >= 5;
    }

    public bool HasAdvantage(Unit rival)
    {
        var rivalWeapon = rival.Weapon;
        var hasAdvantage = ((Weapon == "Sword" && rivalWeapon == "Axe") ||
                (Weapon == "Lance" && rivalWeapon == "Sword") ||
                (Weapon == "Axe" && rivalWeapon == "Lance"));
        if (hasAdvantage)
            _view.WriteLine($"{Name} ({Weapon}) tiene ventaja " +
                            $"con respecto a {rival.Name} ({rival.Weapon})");
        return hasAdvantage;
    }

    public void ApplySkills()
    {
        foreach (var skillName in Skills)
        {
            var skill = new Skill(skillName, this);
            skill.Apply();
        }
    }

    public void AlterStats()
    {
        foreach (var effect in SetAlterEffects())
            StatsManager.AlterUnitStats(effect);
    }

    private List<string> SetAlterEffects()
    {
        var effects = new List<string>();
        if (InFirstRound) effects.Add("AlterBaseStats");
        effects.Add($"Bonus");
        effects.Add($"Penalty");
        var state = "";
        if (InFirstAttack) state = "InFirstAttack";
        if (InFollowUp) state = "InFollowUp";
        effects.Add($"Bonus{state}");
        effects.Add($"Penalty{state}");
        return effects;
    }

    public void PrintSkillsMessages()
    {
        StatsManager.PrintMessages();
    }
    
    public void ResetStats()
    {
        Atk = InitialStats["Atk"];
        Spd = InitialStats["Spd"];
        Def = InitialStats["Def"];
        Res = InitialStats["Res"];
    }

    public void ResetStatsManager()
    {
        StatsManager.ResetStatsDictionary();
    }
}