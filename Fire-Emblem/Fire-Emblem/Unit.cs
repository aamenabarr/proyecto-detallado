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
    public List<Skill> Skills;
    public Unit LastRival;
    public Unit Rival;
    public bool IsAttacker = false;
    public int InitialHp;
    public int InitialAtk;
    public int InitialSpd;
    public int InitialDef;
    public int InitialRes;
    private readonly View _view;
    public EffectManager EffectManager;

    public Unit(AuxUnit unit, List<Skill> skills, View view)
    {
        Name = unit.Name;
        Weapon = unit.Weapon;
        Gender = unit.Gender;
        DeathQuote = unit.DeathQuote;
        Hp = Int(unit.HP);
        Atk = Int(unit.Atk);
        Spd = Int(unit.Spd);
        Def = Int(unit.Def);
        Res = Int(unit.Res);
        InitialHp = Int(unit.HP);
        InitialAtk = Int(unit.Atk);
        InitialSpd = Int(unit.Spd);
        InitialDef = Int(unit.Def);
        InitialRes = Int(unit.Res);
        Skills = skills;
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
    
    private static int Int(string stat)
    {
        return Convert.ToInt32(stat);
    }

    public void SetSkillsEffects()
    {
        foreach (var skill in Skills)
            skill.SetConditionsAndEffects();
    }

    public void ApplySkills()
    {
        foreach (var skill in Skills)
            skill.Apply();
    }

    public void ResetStats()
    {
        foreach (var skill in Skills)
            skill.ResetStats();
    }

    public void ResetFirstAttackEffects()
    {
        foreach (var skill in Skills)
        foreach (var effect in skill.Effects)
            if (effect.InFirstAttack) effect.Reset();
    }
}