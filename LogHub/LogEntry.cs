namespace LogHub;
public class LogEntry
{
    public int Priority { get; set; }
    public int Facility => Priority / 8;
    public int Severity => Priority % 8;
    public string Message { get; set; } = string.Empty;
}