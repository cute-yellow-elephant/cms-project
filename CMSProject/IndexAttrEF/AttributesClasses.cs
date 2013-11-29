using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexAttrEF
{
    public class IndexAttribute : Attribute { }
    public class FullTextIndexAttribute : Attribute { }
    public class FullTextIndex
    {
        public enum SearchAlgorithm
        {
            Contains,
            FreeText
        }
    }
}
