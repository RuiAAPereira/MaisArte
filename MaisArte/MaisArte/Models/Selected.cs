using SQLite;
using System;

namespace MaisArte.Models
{
    public class Selected
    {
        public Selected()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        [PrimaryKey]
        public Guid Id { get; set; }

        public string Item { get; set; }
        public Guid ItemId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}