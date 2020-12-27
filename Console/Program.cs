using DataLayer.Models;
using DataLayer.Process.Interface;
using DataLayer.Process;
using DataLayer;
using Console;

Startup startup = Startup.Create();
startup.Load();
string connection = startup.Connection;

System.Console.WriteLine("Start: insert resume insert!");

ShowCaseContext context = new ShowCaseContext(connection);
IResumeDB resumeDB = new ResumeDB(context);
resumeDB.AddInfoAsync(new ResumeInfo() {
    Name = "Oscar Veldman"
});

System.Console.WriteLine("Finished: insert resume insert!");


