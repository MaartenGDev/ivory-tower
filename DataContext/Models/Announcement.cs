using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DataContext.Models
{
    public class Announcement
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime PublishedAt { get; set; }
        public int CategoryId { get; set; }
        public virtual AnnouncementCategory Category { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}