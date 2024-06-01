namespace Fire_Emblem_Controller;

public class EndOfGame : Exception
{
    public EndOfGame(string message) : base(message) { }
}