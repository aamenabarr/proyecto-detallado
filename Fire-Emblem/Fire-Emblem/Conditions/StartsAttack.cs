namespace Fire_Emblem;

public class StartsAttack : Condition
{
    public StartsAttack(Unit unit) : base(unit){}
    
    public override bool IsMet()
    {
        return Unit.IsAttacker;
    }
}