using System.ComponentModel.DataAnnotations;

namespace Demo_app_web.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        // foreign key to User
        public int UserID { get; set; }

        public virtual User User { get; set; }

        // foreign key to Category
        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

    }
}
