using System;
using System.IO;
namespace LogHub;
public interface ISink { void Write(LogEntry entry); }
public class ConsoleSink : ISink { public void Write(LogEntry entry) => Console.WriteLine($"[{entry.Severity}] {entry.Message}"); }
public class FileSink : ISink 
{ 
    private readonly string _path; 
    public FileSink(string path) => _path = path; 
    public void Write(LogEntry entry) => File.AppendAllText(_path, $"[{entry.Severity}] {entry.Message}\n"); 
}