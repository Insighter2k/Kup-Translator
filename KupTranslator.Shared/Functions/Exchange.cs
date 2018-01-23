using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KupTranslator.Shared.Models;

namespace KupTranslator.Shared.Functions
{
    public class Exchange
    {
        #region Extract

        private static Regex regExSpanMatcher = new Regex(@"(\s*\w*([^<])<\/span>)", RegexOptions.Compiled);
        public static async Task ToReferenceNames(string sourceFile, string targetFile, string wikia, int from, int to, bool recursiveCheck, bool matchByteLength)
        {
            Settings.LogTime = DateTime.Now;
            IO.Write.Log($"SourceFilepath: {sourceFile}", true);
            IO.Write.Log($"TargetFilePath: {targetFile}", true);
            IO.Write.Log($"Wikia: {wikia}", true);
            IO.Write.Log($"Recursive Check: {recursiveCheck}", true);
            IO.Write.Log($"Match ByteLength: {matchByteLength}", true);


            IO.Write.Log("Loading KUP file", true);
            var kup = Kontract.KUP.Load(sourceFile);

            int kupEntryCount = kup.Count;
            if (from == -1) from = 0;
            if (to == -1) to = kupEntryCount;

            IO.Write.Log($"From Count: {from}", true);
            IO.Write.Log($"To Count: {to}", true);


            var entries = kup.Entries.Where(x =>
                Convert.ToInt32(x.Name.Remove(0, 4)) >= from && Convert.ToInt32(x.Name.Remove(0, 4)) <= to).ToList();

            IO.Write.Log("Begin translation", true);
            List<NameExchange> NameExchangeList = new List<NameExchange>();
            foreach (var entry in entries)
            {
                NameExchange nameExchange = new NameExchange();

                nameExchange.Encoding = Encoding.ASCII;
                nameExchange.KupReference = entry.Name;
                nameExchange.OriginalName = entry.OriginalText;
                nameExchange.OriginalNameWordList = nameExchange.OriginalName.Split(' ').ToList();

                if (recursiveCheck)
                {
                    var preCheck = NameExchangeList.FirstOrDefault(x =>
                        x.OriginalNameWordList.Contains(entry.OriginalText) && x.ReferenceName != null);

                    if (preCheck != null)
                    {
                        if (nameExchange.OriginalNameWordList.Count == 1)
                        {
                            nameExchange.ReferenceName = (nameExchange.OriginalNameWordList.Count == 1)
                                ? preCheck.ReferenceNameWordList[preCheck.ReferenceNameWordList.Count - 1]
                                : preCheck.ReferenceName;
                            nameExchange.ReferenceNameWordList = nameExchange.ReferenceName.Split(' ').ToList();
                        }
                    }

                    else Translate.OriginalToReference(wikia, nameExchange);
                }

                else Translate.OriginalToReference(wikia, nameExchange);

                if (matchByteLength && nameExchange.ReferenceName != null) ByteLengthFitter(nameExchange);

                IO.Write.Log($"{nameExchange.OriginalName} => {nameExchange.ReferenceName}");

                NameExchangeList.Add(nameExchange);
            }

            await Task.Run(() => Shared.IO.Write.NameExchangeList(NameExchangeList, targetFile));
        }

        private static NameExchange ByteLengthFitter(NameExchange nameExchange)
        {
            nameExchange.ReferenceName = nameExchange.ReferenceName.Replace(" ", "");
            if (nameExchange.Check)
            {
                switch (nameExchange.ReferenceNameWordList.Count)
                {
                    case 1:
                        nameExchange.ReferenceName = nameExchange.ReferenceNameWordList[0]
                            .Substring(0, nameExchange.OriginalNameLength);
                        break;

                    case 2:
                        nameExchange.ReferenceName = nameExchange.ReferenceNameWordList[1];
                        if (nameExchange.Check)
                            nameExchange.ReferenceName =
                                nameExchange.ReferenceName.Substring(0, nameExchange.OriginalNameLength);
                        break;
                }

            }

            return nameExchange;
        }

        #endregion

        #region Inject

        public static async Task OriginalWithReference(string sourceFile, string targetFile)
        {
            IO.Write.Log("Reading CSV-file",true);
            var namesList = await IO.Read.CsvFile(sourceFile);

            IO.Write.Log("Reading target file", true);
            var targeFileBytes = await IO.Read.FileToByteArray(targetFile);

            IO.Write.Log("Replacing values", true);
            targeFileBytes = await Exchange.BytesFromTargetFile(namesList, targeFileBytes, true);

            IO.Write.Log("Writing \"new\" target file", true);
            IO.Write.ByteArrayToFile(targeFileBytes, targetFile);

        }

        public static Task<byte[]> BytesFromTargetFile(List<NameExchange> list, byte[] targetFile, bool excludeBrackets)
        {
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.ReferenceName))
                {
                    foreach (var offset in Offsets(targetFile, item.OriginalNameBytes, excludeBrackets))

                        for (int i = 0; i < item.OriginalNameBytes.Length; i++)
                        {

                            targetFile[offset + i] = (item.ReferenceNameBytes.Length - 1 >= i)
                                ? item.ReferenceNameBytes[i]
                                : Convert.ToByte(0);
                        }
                }
            }

            return Task.FromResult(targetFile);
        }

        private static IEnumerable<int> Offsets(byte[] data, byte[] toFind, bool excludeBrackets)
        {
            for (int i = 0; i <= data.Length - toFind.Length; ++i)
            {
                bool matched = true;

                for (int j = 0; j < toFind.Length; ++j)
                {
                    if (data[i + j] != toFind[j])
                    {
                        matched = false;

                        break;
                    }

                    if (excludeBrackets)
                    {
                        if (data[i + j] == toFind[j] && data[(i + j) - 1] == Encoding.ASCII.GetBytes("(")[0])
                        {
                            matched = false;

                            break;
                        }
                    }
                }

                if (matched)
                    yield return i;
            }
        }

        #endregion

    }
}
