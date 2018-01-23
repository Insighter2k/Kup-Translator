using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Kontract;
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

            System.IO.File.AppendAllText(filename,output);
            if(displayOnConsole) Console.WriteLine(output);
        }

        public static void NameExchangeList(List<NameExchange> names, string filename)
        {
            Log($"Writing CSV-file to {filename}", true);

            string output = "KupReference;Original Name;Length;Reference Name;Length;Check"+Environment.NewLine;
            foreach (var name in names)
            {
                output = output +
                         $"{name.KupReference};{name.OriginalName};{name.OriginalNameLength};{name.ReferenceName};{name.ReferenceNameLength};{name.Check}" +
                         Environment.NewLine;
               
            }

            if (File.Exists(filename)) File.Delete(filename);
            File.AppendAllText(filename, output);
        }

        public static void NameExchangeList(List<Entry> entries, string filename)
        {
            List<NameExchange> list = new List<NameExchange>();
            foreach (var entry in entries)
            {
                var name = new NameExchange()
                {
                    Encoding = Encoding.Unicode,
                    OriginalName = entry.OriginalText,
                    ReferenceName = entry.EditedText
                };
                list.Add(name);

            }

            NameExchangeList(list, $"{filename.Trim()}.csv");
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
