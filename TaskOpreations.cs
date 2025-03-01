using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_cli
{
    public class TaskOpreations
    {
        public void Add(string description)
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            if (tasks.Count() == 0)
                tasks.Add(new Task(description, 1));
            else
                tasks.Add(new Task(description, tasks[tasks.Count() - 1].Id + 1));

            Console.WriteLine("A new task was added successfully");

            jsonHandler.WriteTask(tasks);
        }

        public void Update(int Id, string description)
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            Task? task = tasks.FirstOrDefault(t => t.Id == Id);

            if (task != null)
            {
                task.Description = description;
                Console.WriteLine("Task was updated successfully");
                jsonHandler.WriteTask(tasks);
            }
            else
            {
                Console.WriteLine("There is no task with this ID");
            }
        }

        public void Delete(int Id)
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            Task? task = tasks.FirstOrDefault(t => t.Id == Id);

            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Task was deleted successfully");
                jsonHandler.WriteTask(tasks);
            }
            else
            {
                Console.WriteLine("There is no task with this ID");
            }
        }

        public void Mark(int taskId, string type)
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            type = type.ToLower();
            Task? task = tasks.FirstOrDefault(t => t.Id == taskId);
            // TODO: Make sure that a Done task cant be in progress

            if (task != null)
            {
                if (type == "mark-done")
                {
                    task.progress = TaskProgress.Done;
                    Console.WriteLine("Task is now marked done");
                }
                else if (type == "mark-in-progress")
                {
                    task.progress = TaskProgress.InProgress;
                    Console.WriteLine("Task is now marked in prgress");
                }
                else
                {
                    Console.WriteLine("Unknown opreation");
                }
                jsonHandler.WriteTask(tasks);
            }
            else
            {
                Console.WriteLine("There is no task with this ID");
            }
        }

        public void List(string type = "")
        {
            JsonFileHandler jsonHandler = new JsonFileHandler("tasks.json");
            List<Task> tasks = jsonHandler.ReadTasks();

            //TODO: Make sure to print the type of the progress not the number
            if (type == "")
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}\nTask progress: {task.progress}");
                    Console.WriteLine("=============");
                }
            }
            else if (type == "done")
            {
                foreach (var task in tasks)
                {
                    if (task.progress == TaskProgress.Done)
                    {
                        Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}");
                        Console.WriteLine("=============");
                    }
                }
            }
            else if (type == "todo")
            {
                foreach (var task in tasks)
                {
                    if (task.progress == TaskProgress.Todo)
                    {
                        Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}");
                        Console.WriteLine("=============");
                    }
                }
            }
            else if (type == "in-progress")
            {
                foreach (var task in tasks)
                {
                    if (task.progress == TaskProgress.InProgress)
                        Console.WriteLine($"Task ID: {task.Id}\nTask description: {task.Description}");
                }
            }
            else
            {
                Console.WriteLine("Invalid list type");
            }
        }
    }
}
