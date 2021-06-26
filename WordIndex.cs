using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniqueWordsApp
{
    public struct WordIndex
    {
        public string Word { get; private set; }
        public int[] AllIndex { get; private set; }
        
    }
}
