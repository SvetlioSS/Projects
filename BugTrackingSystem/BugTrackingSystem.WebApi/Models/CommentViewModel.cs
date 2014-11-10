namespace BugTrackingSystem.WebApi.Models
{
    using System;

    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int BugId { get; set; }

        public string AuthorId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}