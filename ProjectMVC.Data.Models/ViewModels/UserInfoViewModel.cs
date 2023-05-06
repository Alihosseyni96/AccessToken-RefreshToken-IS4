using ProjectMVC.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Data.Models.ViewModels
{
    public class UserInfoViewModel
    {

        //From Api Call
        public int UserId { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }

        public DateTime BirthDate { get; set; }
        public string Address { get; set; }


        //



    }
}
