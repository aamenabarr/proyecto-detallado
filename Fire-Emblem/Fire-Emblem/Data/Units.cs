namespace Fire_Emblem;

public class Units
{
    private List<AuxUnit> _units;
    
    public Units()
    {
        _units = Utils.LoadFromJsonFile<AuxUnit>("characters.json");
    }

    public AuxUnit GetUnit(string name)
    {
        return _units.FirstOrDefault(auxUnit => auxUnit.Name == name);
    }
}