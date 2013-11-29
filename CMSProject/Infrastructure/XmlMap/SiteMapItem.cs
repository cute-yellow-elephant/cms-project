using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsureThat;


namespace Infrastructure.XmlMap
{
     public enum SitemapChangeFrequency
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }

    public class SiteMapItem:ISiteMapItem
    {
        public SiteMapItem(string url, DateTime? lastModified = null, SitemapChangeFrequency? changeFrequency = null, double? priority = null)
        {
            //String.IsNullOrEmpty
            Ensure.That(url,"url").IsNotNullOrEmpty();
            Url = url;
            LastModified = lastModified;
            ChangeFrequency = changeFrequency;
            Priority = priority;
        }

        public string Url { get; protected set; }

        public DateTime? LastModified { get; protected set; }

        public SitemapChangeFrequency? ChangeFrequency { get; protected set; }

        public double? Priority { get; protected set; }
    }
}
