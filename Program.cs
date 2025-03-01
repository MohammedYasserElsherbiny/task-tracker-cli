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
                    opreations.List(args[1]);
                }
                else
                {
                    opreations.List();
                }
            }
            else if(opreation == "mark-in-progress" || opreation == "mark-done")
            {
                TaskOpreations opreations = new TaskOpreations();
                opreations.Mark(Int32.Parse(args[1]), args[0]);
            }
            else
            {
                Console.WriteLine("please provide a valid opreation");
            }
        }
    }
}
