using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models.DTOs
{
    public class PersonDetailDTO
    {
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }

        public DateTime BirthDate { get; set; }
        public string Address { get; set; }

    }
}
