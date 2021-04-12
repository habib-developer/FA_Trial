using System;

namespace FA.Core.Domain
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}
