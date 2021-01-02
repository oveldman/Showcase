using System;
using DataLayer;
using DataLayer.Models;
using DataLayer.Process;

namespace Console
{
    public class Inserter
    {
        private ShowCaseContext _context;

        public Inserter(ShowCaseContext context) 
        {
            _context = context;
        }

        public void Insert() {
            ResumeDB resumeDB = new ResumeDB(_context);
            resumeDB.AddInfoAsync(new ResumeInfo() {
                Name = "Oscar Veldman",
                LivingPlace = "Rotterdam",
                Nationality = "Dutch"
            });

            _context.Users.Add(new ShowCaseUser { 
                UserName = "test@test.nl",
                PasswordHash = "hash",
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                Email = "test@test.nl",
                EmailConfirmed = true,
                NormalizedEmail = "TEST@TEST.NL",
                NormalizedUserName = "TEST@TEST.NL",
            });

            _context.SaveChanges();
        }
    }
}
