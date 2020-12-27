using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Process.Interface;

namespace BusinessLayer.Manager 
{
    public class ResumeManager : IResumeManager {
        private IResumeDB _resumeDB;
        public ResumeManager(IResumeDB resumeDB) {
            _resumeDB = resumeDB;
        }

        public async Task<ResumeInfo> GetInfoAsync()
        {
            return await _resumeDB.GetInfoAsync();
        }
    }
}