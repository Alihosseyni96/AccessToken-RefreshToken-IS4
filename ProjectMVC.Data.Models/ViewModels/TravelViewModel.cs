using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Data.Models.ViewModels
{
    public class TravelViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Origin { get; set; } = null!;

        public DateTime DepartedDate { get; set; }

        public string Destenition { get; set; } = null!;

        public DateTime ReturnDate { get; set; }

    }
}
