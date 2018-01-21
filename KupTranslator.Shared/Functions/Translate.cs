﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KupTranslator.Shared.IO;
using KupTranslator.Shared.Models;
using Convert = System.Convert;

namespace KupTranslator.Shared.Functions
{
    public class Translate
    {
        private static Regex regExSpanMatcher = new Regex(@"(\s*\w*([^<])<\/span>)", RegexOptions.Compiled);
        private static KakasiSettings _kakasiSettings;

        public static async Task Text(string filepath, string wikia, string language, int from, int to)
        {
            Settings.LogTime = DateTime.Now;
            IO.Write.Log($"Filepath: {filepath}");
            IO.Write.Log($"Wikia: {wikia}");
            IO.Write.Log($"Language: {language}");

            if (language == "ro")
            {
                _kakasiSettings = new KakasiSettings();
                Write.Log("Romaji settings:" + string.Join(" ", IO.Convert.KakasiParamsHelper(_kakasiSettings)));
                Kakasi.NET.Interop.KakasiLib.Init();
            }

            IO.Write.Log("Loading KUP file");
            var kup = Kontract.KUP.Load(filepath);

            int kupEntryCount = kup.Count;
            if (from == -1) from = 0;
            if (to == -1) to = kupEntryCount;

            IO.Write.Log($"From Count: {from}");
            IO.Write.Log($"To Count: {to}");

            var entries = kup.Entries.Where(x =>
                Convert.ToInt32(x.Name.Remove(0, 4)) >= from && Convert.ToInt32(x.Name.Remove(0, 4)) <= to).ToList();

            IO.Write.Log("Begin translation");

            foreach (var entry in entries)
            {
                bool isChanged = false;
                if (wikia != string.Empty)
                {
                    var textOri = entry.OriginalText;

                    textOri = textOri.Replace("[", "").Replace("]", "");

                    var textOriArray = textOri.Split('　');
                    textOri = string.Empty;

                    foreach (string text in textOriArray)
                    {
                        if (text.Contains('/'))
                        {
                            textOri = textOri + text.Split('/')[0];
                        }

                        else textOri = textOri + text;
                    }

                    var charCount = textOri.Length;
                    var wikiaResult = WikiaCSharpWrapper.Client.RequestValuesFromWiki(wikia, textOri, false);

                    if (wikiaResult.Result != null)
                    {
                        foreach (var item in wikiaResult.Result.items)
                        {
                            var matches = regExSpanMatcher.Matches(item.snippet);
                            double percentage = ((double) matches.Count / charCount) * 100;
                            if (percentage == 100.00)
                            {
                                entry.EditedText = item.title;
                                isChanged = true;
                                break;
                            }

                            else
                            {
                               var tempMatchVariable = string.Empty;
                               foreach(Match match in matches)
                               {
                                   tempMatchVariable = tempMatchVariable + match.Value.Split('<')[0];
                               }

                                if (tempMatchVariable == textOri)
                                {
                                    entry.EditedText = item.title;
                                    isChanged = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (!isChanged)
                {
                    switch (language)
                    {
                        case "ro":
                            await IO.Convert.ToRomaji(entry, _kakasiSettings);
                            break;
                        case "en":
                            await IO.Convert.ToEnglish(entry);
                            break;
                    }
                }

                Write.Log($"{entry.OriginalText} => {entry.EditedText}");
            }

            if (language == "ro") Kakasi.NET.Interop.KakasiLib.Dispose();

            IO.Write.Log("Saving KUP file");
            kup.Save(filepath);
        }
    }
}
