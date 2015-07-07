using System;
using System.Collections.Generic;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models
{
    public partial class Filter
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string FilterName { get; set; }
        public string FilterString { get; set; }
        public string SortString { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public string Description { get; set; }
    }
}
