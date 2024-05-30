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
        if (_role == "") return _unit.InFirstCombat;
        if (_role == "Attacker" && _unit.FirstAttackerCombat == 1) return true;
        if (_role == "Defender" && _unit.FirstDefenderCombat == 1) return true;
        return false;
    }
}