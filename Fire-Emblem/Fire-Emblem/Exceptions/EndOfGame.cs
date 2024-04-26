namespace Fire_Emblem;

public class EndOfGame : Exception
{
    public EndOfGame(string message) : base(message) { }
}