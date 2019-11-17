using System.ComponentModel.DataAnnotations;

namespace DataContext.Models
{
    public class Contributor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int EditCount { get; set; }
        [Required]
        public int CommentCount { get; set; }
    }
}