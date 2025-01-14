using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace task_cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide an input");
                return;
            }

            string opreation = args[0].ToLower();   
        }
    }

    public class Task
    {
        public string Description { get; set; }
        public int Id {  get; set; }
        public int progress { get; set; }

        public Task(string IncomingDescription)
        {
            Description= IncomingDescription;
            progress = 0;

        }
        // 0 for not done, 1 for in progress, 2 for done

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
