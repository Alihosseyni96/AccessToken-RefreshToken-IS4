using ProjectApi.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models.Services
{
    public interface IPersonServices
    {
        Task<PersonDetailDTO> GetPersonDetials(string nationalCode);

    }
}
