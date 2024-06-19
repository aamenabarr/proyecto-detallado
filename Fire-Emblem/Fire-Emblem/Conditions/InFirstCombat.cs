namespace Fire_Emblem;

public class InFirstCombat : Condition
{
    private string _role = "";
    public InFirstCombat(Unit unit) : base(unit){}

    public InFirstCombat(Unit unit, string role) : base(unit)
    {
        _role = role;
    }
    
    public override bool IsMet()
    {
        if (_role == "") return Unit.InFirstCombat;
        if (_role == "Attacker" && Unit.FirstAttackerCombat == 1) return true;
        if (_role == "Defender" && Unit.FirstDefenderCombat == 1) return true;
        return false;
    }
}