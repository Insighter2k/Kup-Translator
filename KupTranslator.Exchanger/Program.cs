using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KupTranslator.Shared.Models;

namespace KupTranslator.Exchanger
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            //byte[] targetFileByteArray = Shared.IO.Read.FileToByteArray(targetFile);
            //targetFileByteArray = Shared.Functions.Exchange.BytesFromTargetFile(NameExchangeList, targetFileByteArray);
            //Shared.IO.Write.ByteArrayToFile(targetFileByteArray, targetFile2);


            string sourceFile = string.Empty;
            string targetFile = string.Empty;
            string wikia = string.Empty;
            int from = -1;
            int to = -1;
            bool recursiveCheck = false;
            bool matchByteLength = false;

            if (args.Length == 0)
            {
                Console.WriteLine("Insufficient parameters.");
                return;
            }

            string[] argsNew = string.Join(" ", args).Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);


            foreach (var arg in argsNew)
            {
                try
                {
                    if (arg.StartsWith("sf:"))
                    {
                        sourceFile = arg.Split(new string[] { "sf:" }, StringSplitOptions.None)[1].Trim();
                        targetFile = $@"{Environment.CurrentDirectory}\Output\{sourceFile.Split('\\').Last()}.csv";
                    }
                    if (arg.StartsWith("from:"))
                        from = Convert.ToInt32(
                            arg.Split(new string[] { "from:" }, StringSplitOptions.None)[1].Trim());
                    if (arg.StartsWith("to:"))
                        to = Convert.ToInt32(arg.Split(new string[] { "to:" }, StringSplitOptions.None)[1].Trim());
                    if (arg.StartsWith("wikia:"))
                        wikia = arg.Split(new string[] { "wikia:" }, StringSplitOptions.None)[1].Trim();
                    if (arg.StartsWith("rc:"))
                        recursiveCheck = Convert.ToBoolean(arg.Split(new string[] { "rc:" }, StringSplitOptions.None)[1].Trim());
                    if (arg.StartsWith("mbl:"))
                        matchByteLength = Convert.ToBoolean(arg.Split(new string[] { "mbl:" }, StringSplitOptions.None)[1].Trim());
                }

                catch (Exception ex)
                {
                    Console.WriteLine(arg);
                    Console.WriteLine(ex);
                }
            }

            if (sourceFile == string.Empty)
            {
                Console.WriteLine("SourceFilepath has not been set.");
                return;
            }

            if (wikia == string.Empty)
            {
                Console.WriteLine("Wikia has not been set.");
                return;
            }

            try
            {
                await Shared.Functions.Exchange.ToReferenceNames(sourceFile, targetFile, wikia, from, to,
                    recursiveCheck, matchByteLength);
            }

            catch (Exception ex)
            {
                Shared.IO.Write.Log(ex.ToString(), true);
            }

            Console.WriteLine("Press a key to exit.");
            Console.ReadLine();


        }
    }
}
