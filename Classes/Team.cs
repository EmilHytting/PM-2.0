using System.Collections.Generic;

namespace PM_2._0.Classes
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TeamWorker> Workers { get; set; } = new();
        public int? CurrentTaskId { get; set; }
        public Task? CurrentTask { get; set; }
        public List<Task> Tasks { get; set; } = new();
    }
}
