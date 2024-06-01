using Fire_Emblem_View;
using Fire_Emblem;

namespace Fire_Emblem_Controller;

public class Player
{
    public int Id;
    public Unit Unit;
    private Team _team;

    public Player(int id, Team team)
    {
        Id = id;
        _team = team;
    }

    public void ChooseUnit(View view)
    {
        view.WriteLine($"Player {Id} selecciona una opción");
        PrintUnitOptions(view);
        var input = Utils.Int(view.ReadLine());
        Unit = _team.GetUnit(input);
    }

    private void PrintUnitOptions(View view)
    {
        for (var i = 0; i < _team.Length(); i++)
            view.WriteLine($"{i}: {_team.GetUnit(i).Name}");
    }

    public void UpdateTeam()
    {
        if (Unit.Hp != 0) return;
        _team.RemoveUnit(Unit);
        CheckExceptions();
    }

    private void CheckExceptions()
    {
        throw _team.Length() == 0 ? new EndOfGame($"Player {Id} sin unidades") : new EndOfRound();
    }
}