using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKupTranslator.IO
{
    public class Write
    {
        public static void Log(string content)
        {
            var filename =
                $@"{Environment.CurrentDirectory}\{SimpleKupTranslator.Program.LogDateTime:yyyy-MM-dd_hhmmss}.log";
            var output = $@"{System.DateTime.Now:yyyy-MM-dd_hh:mm:ss} - {content}{Environment.NewLine}";

            System.IO.File.AppendAllText(filename,output);
            Console.WriteLine(output);
        }
    }

}
