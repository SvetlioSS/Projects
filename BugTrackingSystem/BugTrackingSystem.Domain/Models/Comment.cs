namespace BugTrackingSystem.Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        public int Id { get; set; }

        public int BugId { get; set; }

        [Required]
        public virtual Bug Bug { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public virtual Author Author { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
