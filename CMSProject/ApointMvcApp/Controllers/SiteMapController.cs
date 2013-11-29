using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.XmlMap;
using ApointMvcApp.Additional;

namespace ApointMvcApp.Controllers
{
    public class SiteMapController : BaseController
    {
        private List<ISiteMapItem> _items;

        public XmlSiteMapResult GetXmlMap()
        {
            _items = new List<ISiteMapItem>();
            AddPages();
            return new XmlSiteMapResult(_items);
        }

        private void AddPages()
        {
            try
            {
                _items.Add(new SiteMapItem(Url.QualifiedRoute("AdminMainPage"), changeFrequency: SitemapChangeFrequency.Monthly, priority: 1.0));
                _items.Add(new SiteMapItem(Url.QualifiedRoute("UserMainPage"), changeFrequency: SitemapChangeFrequency.Monthly, priority: 1.0));
                foreach (var p in core.PostRepository.ReadAll().Where(p=>!p.IsDeleted).OrderByDescending(p=>p.ID).ToList())
                {
                    _items.Add(new SiteMapItem(Url.QualifiedRoute("ViewPost", new { id= p.ID }), changeFrequency: SitemapChangeFrequency.Weekly, priority: 1.0));
                }
            }
            catch { return;  }
        }


    }
}
