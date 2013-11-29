using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.XmlMap
{
    public interface ISiteMapGenerator
    {
        XDocument GenerateSiteMap(IEnumerable<ISiteMapItem> items);
    }
}
