namespace BugTrackingSystem.Web.Models
{
    using System.Collections.Generic;

    public class PagingViewModel
    {
        public int BugStartingIndex { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<BugViewModel> ViewModels { get; set; }

        public bool ShowDetails { get; set; }
    }
}