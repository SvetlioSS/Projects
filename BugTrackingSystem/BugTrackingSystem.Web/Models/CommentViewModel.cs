namespace BugTrackingSystem.Web.Models
{
    using System.Collections.Generic;

    using BugTrackingSystem.Domain.Models;

    public class CommentViewModel
    {
        public PagingViewModel PagingViewModel { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}