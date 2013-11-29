using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Infrastructure.XmlMap;
using System.Globalization;
using EnsureThat;
using System.Text;
using System.Xml;

namespace ApointMvcApp.Additional
{
    public class XmlSiteMapResult:ActionResult
    {
        private readonly IEnumerable<ISiteMapItem> items;
        private readonly ISiteMapGenerator generator;

        public XmlSiteMapResult(IEnumerable<ISiteMapItem> items) : this(items, new SiteMapGenerator())
        {
        }

        public XmlSiteMapResult(IEnumerable<ISiteMapItem> items, ISiteMapGenerator generator)
        {
            Ensure.That(items, "items").IsNotNull();
            Ensure.That(generator, "generator").IsNotNull();

            this.items = items;
            this.generator = generator;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            response.ContentType = "text/xml";
            response.ContentEncoding = Encoding.UTF8;

            using (var writer = new XmlTextWriter(response.Output))
            {
                writer.Formatting = Formatting.Indented;
                var sitemap = generator.GenerateSiteMap(items);

                sitemap.WriteTo(writer);
            }
        }
    }
}