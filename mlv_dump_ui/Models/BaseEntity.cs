using System;
using mlv_dump_ui.Entitis;

namespace mlv_dump_ui.Models
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
