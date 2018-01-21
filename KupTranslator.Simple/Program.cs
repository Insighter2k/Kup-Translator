using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace KupTranslator.Simple
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string filepath = string.Empty;
            string language = string.Empty;
            int from = -1;
            int to = -1;

            if (args.Length == 0)
            {
                Console.WriteLine("Insufficient parameters.");
                return;
            }

            string[] argsNew = string.Join(" ",args).Split(new string[] {"/"}, StringSplitOptions.RemoveEmptyEntries);


            foreach(var arg in argsNew)
            {
                try
                {
                    if (arg.StartsWith("file:"))
                        filepath = arg.Split(new string[]{"file:"}, StringSplitOptions.None)[1];
                    if (arg.StartsWith("lng:"))
                        language = arg.Split(new string[] { "lng:" }, StringSplitOptions.None)[1].Trim();
                    if (arg.StartsWith("from:"))
                        from = Convert.ToInt32(
                            arg.Split(new string[] { "from:" }, StringSplitOptions.None)[1].Trim());
                    if (arg.StartsWith("to:"))
                        to = Convert.ToInt32(arg.Split(new string[] { "to:" }, StringSplitOptions.None)[1].Trim());
                }

                catch (Exception ex)
                {
                    Console.WriteLine(arg);
                    Console.WriteLine(ex);
                }
            }

            if (filepath == string.Empty)
            {
                Console.WriteLine("Filepath could not been set.");
                return;
            }

            if (language == string.Empty)
            {
                Console.WriteLine("Language has not been set. Please enter ro (Romaji) or en (English)");
                return;
            }

            try
            {
                await Shared.Functions.Translate.Text(filepath, language, from, to);
            }

            catch (Exception ex)
            {
                Shared.IO.Write.Log(ex.ToString());
            }

            Console.WriteLine("Press a key to exit.");
            Console.ReadLine();
        }
    }
}
