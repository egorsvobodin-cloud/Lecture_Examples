using LogHub;
var processor = new LogProcessor(new SyslogParser(), new ConsoleSink());
var lines = new[] { "<34>1 2026-11-26T22:14:15.003Z host app - - - Hello syslog", "bad line" };
processor.Process(lines);
Console.WriteLine($"Processed: {processor.ProcessedCount}, Skipped: {processor.SkippedCount}");