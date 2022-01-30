using SQLite;
using System;

namespace MaisArte.Models
{
    public class Work
    {
        public Work()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        [PrimaryKey]
        public Guid Id { get; set; }

        [Indexed]
        public Guid CategoryId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public bool Favorite { get; set; } = false;
        public string FavoriteIcon { get; set; } = "FavoriteEmpty.png";
        public DateTime CreatedAt { get; set; }
    }
}