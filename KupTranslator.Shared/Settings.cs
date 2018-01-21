using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupTranslator.Shared
{
    public class Settings
    {
        public static DateTime LogTime { get; set; }

        public static string LogPath => $@"{Environment.CurrentDirectory}\Log\";
    }
}
