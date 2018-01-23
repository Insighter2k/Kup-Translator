using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupTranslator.Shared.Models
{
    public class NameExchange
    {
        public Encoding Encoding { get; set; }
        public string KupReference { get; set; }
        public string OriginalName { get; set; }
        public string ReferenceName { get; set; }
        public List<string> OriginalNameWordList { get; set; }
        public List<string> ReferenceNameWordList { get; set; }
        public int OriginalNameLength => Functions.Check.ForByteLength(OriginalName);
        public int? ReferenceNameLength => ReferenceName?.Length;
        public bool Check => (ReferenceNameLength.HasValue) && ReferenceNameLength.Value > OriginalNameLength;

        public byte[] OriginalNameBytes => Encoding.GetBytes(OriginalName);
        public byte[] ReferenceNameBytes => (ReferenceName == null) ? null : Encoding.ASCII.GetBytes(ReferenceName);
    }
}
