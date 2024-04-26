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
    public int InitialHp;
    public int InitialAtk;
    public int InitialSpd;
    public int InitialDef;
    public int InitialRes;
    public double Wtb;
    public bool IsAttacker = false;
    public bool InFirstRound = true;
    public List<Skill> Skills;
    public Unit LastRival;
    public Unit Rival;
    private View _view;
    public EffectManager EffectManager;
    private Utils _utils = new Utils();

    public Unit(AuxUnit unit, List<Skill> skills, View view)
    {
        Name = unit.Name;
        Weapon = unit.Weapon;
        Gender = unit.Gender;
        DeathQuote = unit.DeathQuote;
        Hp = _utils.Int(unit.HP);
        Atk = _utils.Int(unit.Atk);
        Spd = _utils.Int(unit.Spd);
        Def = _utils.Int(unit.Def);
        Res = _utils.Int(unit.Res);
        InitialHp = _utils.Int(unit.HP);
        InitialAtk = _utils.Int(unit.Atk);
        InitialSpd = _utils.Int(unit.Spd);
        InitialDef = _utils.Int(unit.Def);
        InitialRes = _utils.Int(unit.Res);
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

    public void SetSkills()
    {
        foreach (var skill in Skills)
            skill.SetSkill();
    }

    public void ResetStats()
    {
        foreach (var skill in Skills)
            skill.Reset();
    }

    public void ResetFirstAttackEffects()
    {
        foreach (var skill in Skills)
        foreach (var effect in skill.Effects)
            if (effect.InFirstAttack) effect.Reset();
    }
}