using System.Collections.Generic;

namespace WikiaCSharpWrapper.Models.API
{
    public class RootObject
    {
        public string batches { get; set; }
        public List<Item> items { get; set; }
        public string total { get; set; }
        public string currentBatch { get; set; }
        public string next { get; set; }
    }
}
