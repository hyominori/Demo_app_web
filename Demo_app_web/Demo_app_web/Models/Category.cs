using System.ComponentModel.DataAnnotations;

namespace Demo_app_web.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<TaskItem> Tasks { get; set; }

    }
}
