using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using KupTranslator.Shared.Models;

namespace KupTranslator.Shared.IO
{
    public class Write
    {
        public static void Log(string content, bool displayOnConsole = false)
        {
            var filename =
                $@"{Settings.LogPath}{Settings.LogTime:yyyy-MM-dd_hhmmss}.log";
            var output = $@"{System.DateTime.Now:yyyy-MM-dd_hh:mm:ss} - {content}{Environment.NewLine}";

            if (!Directory.Exists(Settings.LogPath)) Directory.CreateDirectory(Settings.LogPath);
            
            System.IO.File.AppendAllText(filename,output);
            if(displayOnConsole) Console.WriteLine(output);
        }

        public static void NameExchangeList(List<NameExchange> names, string filename)
        {
            if (!Directory.Exists(@"{Environment.CurrentDirectory}\Output\")) Directory.CreateDirectory(@"{Environment.CurrentDirectory}\Output\");

            Log($"Writing CSV-file to {filename}", true);
            
            foreach (var name in names)
            {
                string output =
                    $"{name.KupReference};{name.OriginalName};{name.OriginalNameLength};{name.ReferenceName};{name.ReferenceNameLength};{name.Check}" +
                    Environment.NewLine;
                File.AppendAllText(filename, output);
            }
        }

        public static void ByteArrayToFile(IEnumerable<byte> array, string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                BinaryWriter writer = new BinaryWriter(fs);
                foreach (byte d in array)
                {
                    writer.Write(d);
                }
            }
        }
    }


}
