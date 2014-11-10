namespace BugTrackingSystem.WebApi.Models
{
    using BugTrackingSystem.Domain.Infrastructure;

    public class UpdateBugViewModel : AddBugViewModel
    {
        public int Id { get; set; }
        
        public BugState? State { get; set; }
    }
}