using System;
using System.Text.RegularExpressions;
namespace LogHub;
public class SyslogParser
{
    private static readonly Regex Regex = new Regex(@"^<(\d{1,3})>\d+\s+\S+\s+\S+\s+\S+\s+\S+\s+(.*)$");
    public LogEntry Parse(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) throw new ArgumentException("Empty");
        var match = Regex.Match(line);
        if (!match.Success) throw new FormatException("Bad format");
        int pri = int.Parse(match.Groups[1].Value);
        if (pri < 0 || pri > 191) throw new ArgumentOutOfRangeException(nameof(pri));
        return new LogEntry { Priority = pri, Message = match.Groups[2].Value };
    }
}