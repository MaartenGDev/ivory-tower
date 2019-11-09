using System.ComponentModel.DataAnnotations;

namespace DataContext.Models
{
    public class AnnouncementCategory
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}