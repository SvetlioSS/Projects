namespace BugTrackingSystem.WebApi.Models
{
    using BugTrackingSystem.Domain.Infrastructure;

    public class AddBugViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Priority? Priority { get; set; }
    }
}