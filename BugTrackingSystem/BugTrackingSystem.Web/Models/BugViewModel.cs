﻿namespace BugTrackingSystem.Web.Models
{
    using System;

    using BugTrackingSystem.Domain.Infrastructure;

    public class BugViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public BugState State { get; set; }

        public Priority Priority { get; set; }

        public string Description { get; set; }

        public DateTime DateIssued { get; set; }
    }
}