using ProjectMVC.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Data.Models.Services
{
    public interface IUserService
    {
        Task<ResultViewModel> UserSignIn(string nationalCoede);
    }
}
