using Fire_Emblem_View;

namespace Fire_Emblem;

public class Player
{
    public int Id;
    public Unit Unit;
    private Team Team;

    public Player(int id, Team team)
    {
        Id = id;
        Team = team;
    }

    public void ChoiceUnit(View view)
    {
        view.WriteLine($"Player {Id} selecciona una opción");
        PrintUnitOptions(view);
        var input = Utils.Int(view.ReadLine());
        Unit = Team.GetUnit(input);
    }

    private void PrintUnitOptions(View view)
    {
        for (var i = 0; i < Team.Length(); i++)
            view.WriteLine($"{i}: {Team.GetUnit(i).Name}");
    }

    public void UpdateTeam()
    {
        if (Unit.Hp != 0) return;
        Team.RemoveUnit(Unit);
        CheckExceptions();
    }

    private void CheckExceptions()
    {
        throw Team.Length() == 0 ? new EndOfGame($"Player {Id} sin unidades") : new EndOfRound();
    }
}