using DataLayer.Models;
using DataLayer.Process.Interface;
using DataLayer.Process;
using DataLayer;
using Console;
using Microsoft.EntityFrameworkCore;

Startup startup = Startup.Create();
startup.Load();

System.Console.WriteLine("Start: insert resume insert!");
startup.Inserter.Insert();
System.Console.WriteLine("Finished: insert resume insert!");


