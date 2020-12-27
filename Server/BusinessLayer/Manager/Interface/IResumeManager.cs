using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Process.Interface;

namespace BusinessLayer.Manager 
{
    public interface IResumeManager {
        Task<ResumeInfo>  GetInfoAsync();
    }
}