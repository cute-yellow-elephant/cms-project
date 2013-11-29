using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using IndexAttrEF;

namespace Domain
{
    public class Post : EntityBase
    {
        //[Index]
        [StringLength(100)]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        //[FullTextIndex]
        public string Content { get; set; }
        public virtual ICollection<Tag> Tags { set; get; }

        public Post() { }
        public Post(string title, string content)
        {
            Title = title;
            Content = content;
            CreationDate = DateTime.UtcNow;
            LastModificationDate = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
