using System;
using System.Collections.Generic;
using System.Text;

namespace PM_2._0.Classes
{
    public class Todo
    {
        public int TodoId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public int? TaskId { get; set; }
        public Task? Task { get; set; }
        public int? WorkerId { get; set; }
        public Worker? Worker { get; set; }
    }
}
