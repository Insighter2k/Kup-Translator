using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupTranslator.Shared.Functions
{
    public class Check
    {
        public static void DirectoriesExist()
        {
            if (!Directory.Exists(Settings.OutputPath)) Directory.CreateDirectory(Settings.OutputPath);
            if (!Directory.Exists(Settings.LogPath)) Directory.CreateDirectory(Settings.LogPath);
        }

        public static int ForByteLength(string value)
        {
            int length = 0;
            foreach (var c in value)
            {
                if (c < 128) length++;
                else length += 4;
            }

            return length;
        }
    }
}
