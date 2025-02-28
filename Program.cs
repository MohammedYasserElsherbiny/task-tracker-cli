using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
namespace task_cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please provide an input");
                return;
            }
            
            //JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            //List<Task> tasks = jsonHandler.ReadTasks();
            
            string opreation = args[0].ToLower();

            if (opreation == "add")
            {
                TaskOpreations opreations = new TaskOpreations();
                opreations.Add(args[1]);
            }
            else if (opreation == "update")
            {
                TaskOpreations opreations = new TaskOpreations();
                opreations.Update(Int32.Parse(args[1]), args[2]);
            }
            else if (opreation == "delete")
            {
                TaskOpreations opreations = new TaskOpreations();
                opreations.Delete(Int32.Parse(args[1]));
            }
            else if (opreation == "list")
            {
                TaskOpreations opreations = new TaskOpreations();
                if (args.Length > 1)
                {

                }
                else
                {

                }
            }
            else if(opreation == "mark-in-progress" || opreation == "mark-done")
            {

            }
            else
            {
                Console.WriteLine("please provide a valid opreation");
            }
        }
    }

    public class Task
    {
        public string Description { get; set; }
        public int Id {  get; set; }
        public int progress { get; set; }

        public Task(string IncomingDescription, int IncomingId)
        {
            Description= IncomingDescription;
            progress = 0;
            IncomingId = Id;
            // 0 for not done, 1 for in progress, 2 for done
        }
    }
    public class TaskOpreations
    {
        public void Add(string description)
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            if (tasks.Count() == 0)
                tasks.Append(new Task(description, 1));
            else
                tasks.Append(new Task(description, tasks[tasks.Count() - 1].Id + 1));

            Console.WriteLine("A new task was added successfully");

            jsonHandler.WriteTask(tasks);
        }

        public void Update(int Id, string description)
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            int isExisted = -1;
            foreach (var task in tasks)
            {
                if (task.Id == Id)
                {
                    isExisted++;
                    task.Description = description;
                    break;
                }
            }

            if (isExisted != -1)
            {
                jsonHandler.WriteTask(tasks);
                Console.WriteLine("Task was updated successfully");
            }

            Console.WriteLine("There is no task with this ID");
        }

        public void Delete(int Id)
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            int taskIdx = -1;
            for (int i = 0; i < tasks.Count(); i++)
            {
                if (tasks[i].Id == Id)
                {
                    taskIdx = i;
                    break;
                }
            }

            if(taskIdx != -1)
            {
                tasks.RemoveAt(taskIdx);
                Console.WriteLine("Task was deleted successfully");
                jsonHandler.WriteTask(tasks);
            }
            else
            {
                Console.WriteLine("There is no task with this ID");
            }
        }

        public void mark(int taskId, string type , List<Task> tasks)
        {
            type = type.ToLower();
            int taskIdx = -1;
            for (int i = 0; i < tasks.Count(); i++)
            {
                if (tasks[i].Id == i)
                {
                    taskIdx= i;
                    break;
                }
            }
           
            if(taskId != -1)
            {
                if(type == "mark-done")
                {
                    tasks[taskIdx].progress = 2;
                    Console.WriteLine("Task is now marked done");
                }
                else if(type == "mark-in-progress")
                {
                    tasks[taskIdx].progress = 1;
                    Console.WriteLine("Task is now marked in prgress");
                }
                else
                {
                    Console.WriteLine("Unknown opreation");
                }
            }
            else
            {
                Console.WriteLine("There is no task with this ID");
            }
        }

        public void list(List<Task> tasks , string type = "")
        {
            if(type == "")
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}\nTask progress: {task.progress}");
                }
            }
            else if (type == "done")
            {
                foreach (var task in tasks)
                {
                    if(task.progress == 2)
                        Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}");
                }
            }
            else if(type == "todo")
            {
                foreach (var task in tasks)
                {
                    if (task.progress == 0)
                        Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}");
                }
            }
            else if(type == "in-progress")
            {
                foreach (var task in tasks)
                {
                    if (task.progress == 1)
                        Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}");
                }
            }
            else
            {
                Console.WriteLine("Invalid list type");
            }
        }
    }


    public class JsonFileHandler
    {
        private readonly string _filePath;

        public JsonFileHandler(string fileName)
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Task> ReadTasks()
        {
            string jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Task>>(jsonData) ?? new List<Task>();
        }

        public void WriteTask(List<Task> tasks)
        {
            string jsonData = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
