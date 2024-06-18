namespace Fire_Emblem;

public class Player
{
    public int Id;
    public Unit Unit;
    public Team Team;

    public Player(int id, Team team)
    {
        Id = id;
        Team = team;
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