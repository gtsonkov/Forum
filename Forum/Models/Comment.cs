using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Update")]
        public DateTime LastUpdatedDate { get; set; }

        [Display(Name = "Topic")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        [ForeignKey("Author")]
        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
