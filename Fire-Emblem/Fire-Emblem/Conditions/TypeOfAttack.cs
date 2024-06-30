namespace Fire_Emblem;

public class TypeOfAttack : Condition
{
    private string _attack;

    public TypeOfAttack(Unit unit, string attack) : base(unit)
        => _attack = attack;
    
    public override bool IsMet()
    {
        return _attack == "Physical" ? Unit.Weapon != Weapons.Magic : Unit.Weapon == Weapons.Magic;
    }
}