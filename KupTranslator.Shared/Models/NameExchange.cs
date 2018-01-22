using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupTranslator.Shared.Models
{
    public class NameExchange
    {
        public string KupReference { get; set; }
        public string OriginalName { get; set; }
        public string ReferenceName { get; set; }
        public List<string> OriginalNameWordList { get; set; }
        public List<string> ReferenceNameWordList { get; set; }
        public int OriginalNameLength => OriginalName.Length;
        public int? ReferenceNameLength => ReferenceName?.Length;
        public bool Check => (ReferenceNameLength.HasValue) && ReferenceNameLength.Value > OriginalNameLength;

        public byte[] OriginalNameBytes => Encoding.ASCII.GetBytes(OriginalName);
        public byte[] ReferenceNameBytes => (ReferenceName != null) ? Encoding.ASCII.GetBytes(ReferenceName) : null;
    }
}
