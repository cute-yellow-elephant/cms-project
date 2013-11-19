using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class EntityBase
    {
        [Key]
        public Guid ID { set; get; }
        public bool IsDeleted { set; get; }

        public EntityBase()
        {
            ID = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
