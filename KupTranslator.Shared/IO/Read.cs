using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KupTranslator.Shared.Models;

namespace KupTranslator.Shared.IO
{
    public class Read
    {
        public static Task<byte[]> FileToByteArray(string filename)
        {
            byte[] buffer = null;
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return Task.FromResult(buffer);
        }

        public static Task<List<NameExchange>> CsvFile(string filename)
        {
            List<NameExchange> list = new List<NameExchange>();
            string content = string.Empty;

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                TextReader tr = new StreamReader(fs, Encoding.ASCII);
                content = tr.ReadToEnd();
                tr.Dispose();
            }

            string[] contentArray = content.Split(new string[] {"\r\n"}, StringSplitOptions.None);

            foreach (var item in contentArray)
            {
                string[] itemArray = item.Split(';');
                var name = new NameExchange()
                {
                    Encoding = Encoding.ASCII,
                    OriginalName = itemArray.First(),
                    ReferenceName = itemArray.Last()
                }; 
                list.Add(name);
            }

            return Task.FromResult(list);
        }
    }
}
