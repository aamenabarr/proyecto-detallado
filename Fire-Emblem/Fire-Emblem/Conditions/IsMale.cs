namespace Fire_Emblem;

public class IsMale : Condition
{
    public IsMale(Unit unit) : base(unit){}
    
    public override bool IsMet()
    {
        return Unit.Gender == "Male";
    }
}