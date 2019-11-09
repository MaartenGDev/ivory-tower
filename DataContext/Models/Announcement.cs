using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Models
{
    public class Announcement
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual AnnouncementCategory Category { get; set; }
    }
}