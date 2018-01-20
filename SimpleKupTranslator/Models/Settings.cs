namespace SimpleKupTranslator.Models
{
    public class Settings
    {
        public bool EnableKanjiToAscii => true;
        public bool EnableHiraganaToAscii => true;
        public bool EnableKatakanaToAscii => true;
        public bool EnableKigouToAscii => true;
        public bool EnableJisRomanToAscii => false;
        public bool EnableKanaToAscii => true;
        public bool EnableGraphicToAscii => false;
        public bool InsertSeparateCharacters => true;
        public bool CapitalizeRomaji => true;
        public bool UpscaleRomaji => false;
        public bool EnableWakitagaki => false;
        public bool EnableHepburn => true;
    }
}
