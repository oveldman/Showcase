using System;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Process.Interface
{
    public interface IResumeDB
    {
        Task<ResumeInfo> GetInfoAsync();
        bool AddInfoAsync(ResumeInfo info);
    }
}
