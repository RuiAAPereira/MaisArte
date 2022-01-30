using SQLite;
using System;

namespace MaisArte.Models
{
    public class Category
    {
        public Category()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        [PrimaryKey]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}