using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                        
            string opreation = args[0].ToLower();

            TaskOpreations operations = new TaskOpreations();
            switch (opreation)
            {
                case "add":
                    operations.Add(args[1]);
                    break;
                case "update":
                    operations.Update(int.Parse(args[1]), args[2]);
                    break;
                case "delete":
                    operations.Delete(int.Parse(args[1]));
                    break;
                case "list":
                    operations.List(args.Length > 1 ? args[1] : "");
                    break;
                case "mark-in-progress":
                case "mark-done":
                    operations.Mark(int.Parse(args[1]), args[0]);
                    break;
                default:
                    Console.WriteLine("Please provide a valid operation.");
                    break;
            }
        }
    }
}
