using System.Collections.Generic;
namespace LogHub;
public class LogProcessor
{
    private readonly SyslogParser _parser;
    private readonly ISink _sink;
    public int ProcessedCount { get; private set; }
    public int SkippedCount { get; private set; }
    public LogProcessor(SyslogParser parser, ISink sink) { _parser = parser; _sink = sink; }
    public void Process(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            try { _sink.Write(_parser.Parse(line)); ProcessedCount++; }
            catch { SkippedCount++; }
        }
    }
}