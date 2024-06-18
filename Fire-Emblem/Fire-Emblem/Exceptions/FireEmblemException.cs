namespace Fire_Emblem;

public abstract class FireEmblemException : Exception
{
    public FireEmblemException(string message) : base(message) { }
}