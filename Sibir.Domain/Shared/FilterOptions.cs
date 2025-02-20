﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sibir.Domain.Shared
{
    public class FilterOptions()
    {
        public string? TitlePart { get; set; }

        public string? CompanyConsumerPart { get; set; }

        public string? CompanyExecuterPart { get; set; }

        public DateOnly? BeginingOfTimeRange { get; set; } 

        public DateOnly? EndOfTimeRange { get; set; }

        public SubjectOfSorting SubjectOfSorting { get; set; } = SubjectOfSorting.None;

        public bool SortDirection { get; set; } = true;

        public string? Status { get; set; }
    }
}
