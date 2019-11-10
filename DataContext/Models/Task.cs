using System.ComponentModel.DataAnnotations;

namespace DataContext.Models
{
    public class Task
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
    }
}