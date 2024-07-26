using Serilog;

namespace Logger;

public static class LogStarter
{
    public static void CreateLogger(string pathToFolder = "", string pathToLogFile = "")
    {
        if (pathToFolder == string.Empty)
            pathToFolder = Environment.CurrentDirectory;

        if (pathToLogFile == string.Empty)
            pathToLogFile = "\\logs_.txt";

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File($"{pathToFolder}{pathToLogFile}", rollingInterval: RollingInterval.Day)
            .MinimumLevel.Debug()
            .CreateLogger();
    }
}
