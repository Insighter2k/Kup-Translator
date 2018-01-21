using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using KupTranslator.Shared.Models;

namespace KupTranslator.Shared.IO
{public class Convert
    {
        private static string[] KakasiParamsHelper(KakasiSettings settings)
        {
            List<string> list = new List<string>();
            list.Add("kakasi");
            if(settings.CapitalizeRomaji) list.Add("-C");
            list.Add(settings.EnableHepburn ? "-rhepburn" : "-rkunrei");
            if (settings.EnableGraphicToAscii) list.Add("-ga");
            if (settings.EnableHiraganaToAscii) list.Add("-Ha");
            if (settings.EnableWakitagaki) list.Add("-w");
            if (settings.EnableJisRomanToAscii) list.Add("-ja");
            if (settings.EnableKanaToAscii) list.Add("-ka");
            if (settings.EnableKatakanaToAscii) list.Add("-Ka");
            if (settings.EnableKigouToAscii) list.Add("-Ea");
            if (settings.EnableKanjiToAscii) list.Add("-Ja");
            if (settings.InsertSeparateCharacters) list.Add("-s");
            if (settings.UpscaleRomaji) list.Add("-U");

            return list.ToArray();
        }

        public static Task<List<Kontract.Entry>> ToRomaji(List<Kontract.Entry> entries, KakasiSettings settings)
        {
            Kakasi.NET.Interop.KakasiLib.Init();
            var kakasiParams = KakasiParamsHelper(settings);
            Kakasi.NET.Interop.KakasiLib.SetParams(kakasiParams);

            var outputKakasiParams = string.Join(" ", kakasiParams);
            Write.Log("Romaji settings:" + outputKakasiParams);

            foreach (var entry in entries)
            {
                try
                {
                    entry.EditedText = Kakasi.NET.Interop.KakasiLib.DoKakasi(entry.OriginalText);
                    Write.Log($"{entry.OriginalText} => {entry.EditedText}");
                }

                catch(Exception ex)
                {
                    Write.Log(ex.ToString());
                }
            }
           
            Kakasi.NET.Interop.KakasiLib.Dispose();

            return Task.FromResult(entries);
        }

        private static Regex regEx = new Regex("\\[\\\"(.+?)\\\"\\]", RegexOptions.Compiled);
        public static Task<List<Kontract.Entry>> ToEnglish(List<Kontract.Entry> entries)
        {
            

            foreach (var entry in entries)
            {
                try
                {
                    string requestValue = entry.OriginalText;

                    if (requestValue.Contains("\\")) requestValue = HttpUtility.UrlEncode(requestValue);
                    var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=ja&tl=en&dt=t&q={requestValue}";

                    WebClient webClient = new WebClient();
                    webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 " +
                                                 "(KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");

                    webClient.Encoding = Encoding.UTF8;

                    var result = webClient.DownloadString(url);
                    result = result.Replace(",null,null,3", "");
                    result = result.Replace(",null,\"ja\"", "");
                    result = result.Replace("\\n", "");

                    MatchCollection matchCollection = regEx.Matches(result);

                    entry.EditedText = matchCollection[0].Value.Split(',')[0];
                    Write.Log($"{entry.OriginalText} => {entry.EditedText}");
                }

                catch(Exception ex)
                {
                    Write.Log(ex.ToString());
                }
            }
            return Task.FromResult(entries);
        }
    }
}
