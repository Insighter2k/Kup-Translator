using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SimpleKupTranslator
{
    class Program
    {
        public static DateTime LogDateTime;
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

            string[] argsNew = string.Join(" ",args).Split(new string[] {"-"}, StringSplitOptions.RemoveEmptyEntries);


            foreach(var arg in argsNew)
            {
                try
                {
                    if (arg.StartsWith("file:"))
                        filepath = arg.Split(new string[]{"file:"}, StringSplitOptions.None)[1];
                    if (arg.StartsWith("lng:"))
                        language = arg.Split(new string[] { "lng:" }, StringSplitOptions.None)[1];
                    if (arg.StartsWith("from:"))
                        from = Convert.ToInt32(
                            arg.Split(new string[] { "from:" }, StringSplitOptions.None)[1]);
                    if (arg.StartsWith("to:"))
                        to = Convert.ToInt32(arg.Split(new string[] { "to:" }, StringSplitOptions.None)[1]);
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
                LogDateTime = System.DateTime.Now;
                IO.Write.Log($"Filepath: {filepath}");
                IO.Write.Log($"Language: {language}");

                var settings = new Models.Settings();
                IO.Write.Log("Loading KUP file");
                var kup = Kontract.KUP.Load(filepath);

                int kupEntryCount = kup.Count;
                if (from == -1) from = 0;
                if (to == -1) to = kupEntryCount;

                IO.Write.Log($"From Count: {from}");
                IO.Write.Log($"To Count: {to}");

                var entries = kup.Entries.Where(x => System.Convert.ToInt32(x.Name.Remove(0, 4)) >= from && System.Convert.ToInt32(x.Name.Remove(0, 4)) <= to).ToList();

                IO.Write.Log("Begin translation");
                if (language == "ro")
                    await IO.Convert.ToRomaji(entries, settings);
                if(language == "en")
                    await IO.Convert.ToEnglish(entries);

                IO.Write.Log("Saving KUP file");
                kup.Save(filepath);
            }

            catch (Exception ex)
            {
                IO.Write.Log(ex.ToString());
            }

            Console.WriteLine("Press a key to exit.");
            Console.ReadLine();

        }
    }
}
