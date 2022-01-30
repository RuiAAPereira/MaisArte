using SQLite;
using System;

namespace MaisArte.Models
{
    public class Item
    {
        public Item()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        [PrimaryKey]
        public Guid Id { get; set; }

        [Indexed]
        public Guid WorkId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}