public class IdisException : Exception
{
    public IdisException(string message, Exception innerException, string line) : base(message, innerException)
    {
        Line = line;
    }
    
    public string? Line { get; set; }
}