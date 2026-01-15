using System;
using System.Collections.Generic;
using System.Text;

namespace PM_2._0.Classes
{
    public class Task
    {

        public int TaskId { get; set; }
        public string Name { get; set; }
        public  List<Todo> Todos { get; set; } = new();
	}
}
