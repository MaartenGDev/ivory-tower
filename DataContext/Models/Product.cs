using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataContext.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}