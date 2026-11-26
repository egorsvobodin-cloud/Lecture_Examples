using Xunit;
using LogHub;
using System;
using System.Collections.Generic;
namespace LogHubTests;
public class LogTests
{
    [Fact] public void Parse_ValidLine_ReturnsEntry() { var e = new SyslogParser().Parse("<34>1 2003-10-11T22:14:15.003Z h a - - - msg"); Assert.Equal(34, e.Priority); Assert.Equal(4, e.Facility); Assert.Equal(2, e.Severity); }
    [Theory][InlineData(0, 0, 0)][InlineData(191, 23, 7)] public void Parse_Boundaries(int p, int f, int s) { var e = new SyslogParser().Parse($"<{p}>1 2003-10-11T22:14:15.003Z h a - - - msg"); Assert.Equal(f, e.Facility); Assert.Equal(s, e.Severity); }
    [Fact] public void Parse_Invalid_Throws() => Assert.Throws<FormatException>(() => new SyslogParser().Parse("bad"));
    [Fact] public void Process_Valid_Writes() { var s = new TestSink(); var p = new LogProcessor(new SyslogParser(), s); p.Process(new[] { "<34>1 2003-10-11T22:14:15.003Z h a - - - m" }); Assert.Equal(1, p.ProcessedCount); }
    [Fact] public void Process_Invalid_Skips() { var p = new LogProcessor(new SyslogParser(), new TestSink()); p.Process(new[] { "bad", "<34>1 2003-10-11T22:14:15.003Z h a - - - m" }); Assert.Equal(1, p.SkippedCount); }
    [Fact] public void Process_Mixed() { var p = new LogProcessor(new SyslogParser(), new TestSink()); p.Process(new[] { "<34>1 2003-10-11T22:14:15.003Z h a - - - m", "bad" }); Assert.Equal(1, p.ProcessedCount); Assert.Equal(1, p.SkippedCount); }
}
public class TestSink : ISink { public List<LogEntry> Written { get; } = new(); public void Write(LogEntry e) => Written.Add(e); }