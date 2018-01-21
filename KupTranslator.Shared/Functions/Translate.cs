using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupTranslator.Shared.Functions
{
    public class Translate
    {
        public static async Task Text(string filepath, string language, int from, int to)
        {
            Settings.LogTime = DateTime.Now;
            IO.Write.Log($"Filepath: {filepath}");
            IO.Write.Log($"Language: {language}");

            var settings = new Models.KakasiSettings();
            IO.Write.Log("Loading KUP file");
            var kup = Kontract.KUP.Load(filepath);

            int kupEntryCount = kup.Count;
            if (from == -1) from = 0;
            if (to == -1) to = kupEntryCount;

            IO.Write.Log($"From Count: {from}");
            IO.Write.Log($"To Count: {to}");

            var entries = kup.Entries.Where(x => Convert.ToInt32(x.Name.Remove(0, 4)) >= from && Convert.ToInt32(x.Name.Remove(0, 4)) <= to).ToList();

            IO.Write.Log("Begin translation");
            switch (language)
            {
                case "ro":
                    await IO.Convert.ToRomaji(entries, settings);
                    break;
                case "en":
                    await IO.Convert.ToEnglish(entries);
                    break;
            }

            IO.Write.Log("Saving KUP file");
            kup.Save(filepath);
        }
    }
}
