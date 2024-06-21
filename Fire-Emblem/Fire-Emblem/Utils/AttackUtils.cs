namespace Fire_Emblem;

public static class AttackUtils
{
    public static int Attack(Unit unit, Unit rival)
    {
        var damage = Math.Max(0, Math.Max(0, Damage(unit, rival)) + (int)Math.Floor(unit.Dmg));
        ReduceHp(rival, damage);
        unit.Damage = damage;
        return damage;
    }

    private static int Defense(Unit unit, Unit rival)
    {
        return rival.Weapon == "Magic" ? unit.Res : unit.Def;
    }

    public static int Damage(Unit unit, Unit rival)
    {
        var attack = (int)Math.Floor(unit.Atk * unit.Wtb);
        return attack - Defense(rival, unit);
    }

    private static void ReduceHp(Unit unit, int damage)
    {
        unit.Hp = Math.Max(0, unit.Hp - damage);
    }
    
    public static bool CanDoFollowUp(Unit unit, Unit rival)
    {
        var rivalSpeed = (rival.Spd);
        return (unit.Spd - rivalSpeed) >= 5;
    }

    public static bool HasAdvantage(Unit unit, Unit rival)
    {
        var sword = "Sword";
        var axe = "Axe";
        var lance = "Lance";
        var rivalWeapon = rival.Weapon;
        var hasAdvantage = ((unit.Weapon == sword && rivalWeapon == axe) ||
                            (unit.Weapon == lance && rivalWeapon == sword) ||
                            (unit.Weapon == axe && rivalWeapon == lance));
        unit.HasWeaponAdvantage = hasAdvantage;
        return hasAdvantage;
    }
}