using Fire_Emblem_View;

namespace Fire_Emblem;

public class Player
{
    public readonly Team Team;
    public readonly int Id;
    public Unit? Unit;
    private readonly View _view;

    public Player(int id, Team team, View view)
    {
        Id = id;
        Team = team;
        _view = view;
    }

    public void ChoiceUnit()
    {
        _view.WriteLine($"Player {Id} selecciona una opción");
        for (var i = 0; i < Team.Units.Count; i++)
            _view.WriteLine($"{i}: {Team.Units[i].Name}");
        Unit = Team.Units[Convert.ToInt32(_view.ReadLine())];
        Unit.IsAttacker = false;
    }

    public void ApplyBaseEffects(EffectManager effectManager)
    {
        foreach (var unit in Team.Units)
        {
            unit.EffectManager = effectManager;
            foreach (var skill in unit.Skills)
            {
                skill.Unit = unit;
                skill.ApplyBaseEffects();
            }
        }
    }

    public void UpdateTeam()
    {
        if (Unit == null || Unit.Hp != 0)
            return;
        Team.Units.Remove(Unit);
        CheckExceptions();
    }

    private void CheckExceptions()
    {
        throw Team.Units.Count == 0 ? new EndOfGame($"Player {Id} sin unidades") : new EndOfRound();
    }
}