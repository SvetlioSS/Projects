namespace BugTrackingSystem.WebApi.Models
{
    public class AddCommentViewModel
    {
        public int BugId { get; set; }

        public string Content { get; set; }
    }
}