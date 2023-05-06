using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Data.Models.ViewModels
{
    public class ResultViewModel
    {
        public List<TravelViewModel> Travels { get; set; }
        public UserInfoViewModel UserInfo { get; set; }
    }
}
