using Fire_Emblem_View;

namespace Fire_Emblem;

public class Player
{
    public int Id;
    public Team Team;
    public Unit Unit;
    private Utils _utils = new Utils();

    public Player(int id, Team team)
    {
        Id = id;
        Team = team;
    }

    public void ChoiceUnit(View view)
    {
        view.WriteLine($"Player {Id} selecciona una opción");
        PrintUnitOptions(view);
        var input = _utils.Int(view.ReadLine());
        Unit = Team.Units[input];
    }

    private void PrintUnitOptions(View view)
    {
        for (var i = 0; i < Team.Units.Count; i++)
            view.WriteLine($"{i}: {Team.Units[i].Name}");
    }

    public void UpdateTeam()
    {
        if (Unit.Hp != 0) return;
        Team.Units.Remove(Unit);
        CheckExceptions();
    }

    private void CheckExceptions()
    {
        throw Team.Units.Count == 0 ? new EndOfGame($"Player {Id} sin unidades") : new EndOfRound();
    }
}