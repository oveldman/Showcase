using DataLayer.Models;
using DataLayer.Process.Interface;
using DataLayer.Process;
using DataLayer;

string connection = "Server=localhost;Port=5432;Database=ShowCaseDB;Username=postgres;Password=mysecretpassword";

System.Console.WriteLine("Start: insert resume insert!");

ShowCaseContext context = new ShowCaseContext(connection);
IResumeDB resumeDB = new ResumeDB(context);
resumeDB.AddInfoAsync(new ResumeInfo() {
    Name = "Oscar Veldman"
});

System.Console.WriteLine("Finished: insert resume insert!");


