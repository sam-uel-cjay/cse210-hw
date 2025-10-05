using System;
using System.IO;

public static class SessionLogger
{
    private static readonly string _logFilePath = "session_log.txt";

    // appends a short record to a text file. this demonstrates the added creativity requirements.
    public static void LogSession(string activityName, int durationSeconds)
    {
        try
        {
            string line = $"{DateTime.Now:u} | Activity: {activityName} | Duration: {durationSeconds}s";
            File.AppendAllText(_logFilePath, line + Environment.NewLine);
        }
        catch
        {
            // if the logging fails (e.g. permission), fail silently so the program will still works.
        }
    }
}
