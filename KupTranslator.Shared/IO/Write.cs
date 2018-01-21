using System;
using System.IO;

namespace KupTranslator.Shared.IO
{
    public class Write
    {
        public static void Log(string content)
        {
            var filename =
                $@"{Settings.LogPath}{Settings.LogTime:yyyy-MM-dd_hhmmss}.log";
            var output = $@"{System.DateTime.Now:yyyy-MM-dd_hh:mm:ss} - {content}{Environment.NewLine}";

            if (!Directory.Exists(Settings.LogPath)) Directory.CreateDirectory(Settings.LogPath);

            System.IO.File.AppendAllText(filename,output);
            Console.WriteLine(output);
        }
    }

}
