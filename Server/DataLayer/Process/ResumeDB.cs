using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Process.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Process
{
    public class ResumeDB : IResumeDB
    {
        private ShowCaseContext _context;
        
        public ResumeDB(ShowCaseContext context) {
            _context = context;
        }

        public async Task<ResumeInfo> GetInfoAsync()
        {
            ResumeInfo resume = await _context.ResumeInfos
                                            .OrderBy(ri => ri.Created)
                                            .LastOrDefaultAsync();

            return resume != null ? resume : new();
        }

        public bool AddInfoAsync(ResumeInfo info)
        {
            info.Created = DateTime.Now;

            _context.ResumeInfos.Add(info);
            _context.SaveChanges();

            return true;
        }
    }
}