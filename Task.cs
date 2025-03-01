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
        public int progress { get; set; }

        [JsonConstructor]
        public Task(string description, int id)
        {
            Description = description;
            progress = 0;
            Id = id;
            // 0 for not done, 1 for in progress, 2 for done
        }
    }
}
