﻿using HR_Board.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Board.Data.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Logo { get; set; }

        public string CompanyName { get; set; }

        public JobStatus Status { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
