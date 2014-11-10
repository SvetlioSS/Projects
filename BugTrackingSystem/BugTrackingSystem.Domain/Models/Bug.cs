namespace BugTrackingSystem.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using BugTrackingSystem.Domain.Infrastructure;

    public class Bug
    {
        private ICollection<Comment> comments;

        public Bug()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name for the bug.")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        public string AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [Required(ErrorMessage = "Please choose a state for the bug.")]
        public BugState State { get; set; }

        [Required(ErrorMessage = "Please choose a priority for the bug.")]
        public Priority Priority { get; set; }

        [Required(ErrorMessage = "Please make a description of the bug.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "none")]
        [Display(Name = "Date issued")]
        public DateTime DateIssued { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }
    }
}
