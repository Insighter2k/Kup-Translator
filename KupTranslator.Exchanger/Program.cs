using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KupTranslator.Shared.Enum;
using KupTranslator.Shared.Models;

namespace KupTranslator.Exchanger
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            string sourceFile = string.Empty;
            string outputFile = string.Empty;
            string targetFile = string.Empty;
            string wikia = string.Empty;
            int from = -1;
            int to = -1;
            bool recursiveCheck = false;
            bool matchByteLength = false;
            Mode mode = Mode.None;
            

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
                        sourceFile = arg.Split(new string[] { "sf:" }, StringSplitOptions.None).Last().Trim();
                        outputFile = $@"{Environment.CurrentDirectory}\Output\{sourceFile.Split('\\').Last()}.csv";
                    }
                    if (arg.StartsWith("from:"))
                        from = Convert.ToInt32(
                            arg.Split(new string[] { "from:" }, StringSplitOptions.None).Last().Trim());
                    if (arg.StartsWith("to:"))
                        to = Convert.ToInt32(arg.Split(new string[] { "to:" }, StringSplitOptions.None).Last().Trim());
                    if (arg.StartsWith("wikia:"))
                        wikia = arg.Split(new string[] { "wikia:" }, StringSplitOptions.None).Last().Trim();
                    if (arg.StartsWith("rc:"))
                        recursiveCheck = Convert.ToBoolean(arg.Split(new string[] { "rc:" }, StringSplitOptions.None).Last().Trim());
                    if (arg.StartsWith("mbl:"))
                        matchByteLength = Convert.ToBoolean(arg.Split(new string[] { "mbl:" }, StringSplitOptions.None).Last().Trim());
                    if (arg.StartsWith("mode:"))
                        mode = (Mode)Enum.Parse(typeof(Mode), arg.Split(new string[] { "mode:" }, StringSplitOptions.None).Last().Trim(), true);
                    if (arg.StartsWith("tf:"))
                        targetFile = arg.Split(new string[] {"tf:"}, StringSplitOptions.None).Last().Trim();

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

            if (wikia == string.Empty && mode == Mode.Extract)
            {
                Console.WriteLine("Wikia has not been set.");
                return;
            }

            if(targetFile == string.Empty && mode == Mode.Inject)
            {
                Console.WriteLine("TargetFilepath has not been set.");
                return;
            }


            try
            {
                Shared.Functions.Check.DirectoriesExist();

                switch(mode)
                {
                    case Mode.Extract:
                        await Shared.Functions.Exchange.ToReferenceNames(sourceFile, outputFile, wikia, from, to,
                            recursiveCheck, matchByteLength);
                        break;
                    case Mode.Inject:
                        await Shared.Functions.Exchange.OriginalWithReference(sourceFile, targetFile);
                        break;
                } 
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
