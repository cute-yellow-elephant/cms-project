using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace SqlRepository.Repositories
{
    public class PostRepository : RepositoryBasis<Post>
    {
        protected override DbSet<Post> _table { get { return _dbContext.Posts; } }
        public PostRepository(DBContextContainer dbContext) : base(dbContext) { }

        public override void Create(Post entity)
        {
            var en = this.Find(entity.Title);
            if (en != null)
                throw new Exception("The post " + en.Title + ", id " + en.ID + " already exists in the database");
            base.Create(entity);
        }

        public override void Update(Post entity)
        {
            var x = this.Read(entity.ID);
            x.Title = entity.Title;
            x.Content = entity.Content;
            x.LastModificationDate = entity.LastModificationDate;
           // x.Tags = entity.Tags;
        }

        public override Post Find(string title)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (String.Compare(x.Title, title) == 0)
                    return x;
            return null;
        }
    }
}
