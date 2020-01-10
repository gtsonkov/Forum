using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Topic
    {
        public List<Comment> Comments { get; set; }
        public Topic()
        {
            Comments = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        [Display(Name ="Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Last Updated Date")]
        public DateTime LastUpdatedDate { get; set; }

        [ForeignKey("Autor")]
        public string AutorId { get; set; }

        public ApplicationUser Autor { get; set; }

        [NotMapped]
        [Display(Name = "Number Coments")]
        public int NumberComents => Comments.Count;
    }
}
