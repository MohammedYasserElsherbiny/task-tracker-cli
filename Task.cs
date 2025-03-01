using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace task_cli
{
    public class Task
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public TaskProgress progress { get; set; }

        [JsonConstructor]
        public Task(string description, int id)
        {
            Description = description;
            Id = id;
            progress = TaskProgress.Todo;
        }
    }
}
