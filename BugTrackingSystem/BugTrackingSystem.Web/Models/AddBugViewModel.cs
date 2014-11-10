namespace BugTrackingSystem.Web.Models
{
    public class AddBugViewModel
    {
        public int BugStartingIndex { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public bool ShowDetails { get; set; }
    }
}