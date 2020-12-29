using DataLayer.Models;
using DataLayer.Process.Interface;
using DataLayer.Process;
using DataLayer;
using Console;
using Microsoft.EntityFrameworkCore;

Startup startup = Startup.Create();
startup.Load();

System.Console.WriteLine("Start: insert resume insert!");

ShowCaseContext context = startup.Context;
IResumeDB resumeDB = new ResumeDB(context);
resumeDB.AddInfoAsync(new ResumeInfo() {
    Name = "Oscar Veldman",
    LivingPlace = "Rotterdam",
    Nationality = "Dutch"
});

System.Console.WriteLine("Finished: insert resume insert!");


