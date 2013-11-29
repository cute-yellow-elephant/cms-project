using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.XmlMap
{
    public interface ISiteMapItem
    {
        string Url { get; }
        DateTime? LastModified { get; }
        SitemapChangeFrequency? ChangeFrequency { get; }
        double? Priority { get; }
    }
}
