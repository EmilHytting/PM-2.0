using System.Collections.Generic;

namespace PM_2._0.Classes
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TeamWorker> Teams { get; set; } = new();
        public int? CurrentTodoId { get; set; }
        public Todo? CurrentTodo { get; set; }
        public List<Todo> Todos { get; set; } = new();
    }
}
