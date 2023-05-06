using Microsoft.EntityFrameworkCore;
using ProjectApi.Data.Models.DTOs;
using ProjectApi.Data.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IPersonRepository _personRepository;

        public PersonServices(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonDetailDTO> GetPersonDetials(string nationalCode)
        {
            var person = await _personRepository.Get(x => x.NationalCode == nationalCode).SingleOrDefaultAsync();
            if (person == null)
            {
                throw new NullReferenceException("National Code is not Valid");
            }
            var res = new PersonDetailDTO()
            {
                Address = person.Address,
                BirthDate = person.BirthDate,
                FatherName = person.FatherName,
                FirstName = person.FirstName,
                Id = person.Id,
                LastName = person.LastName,
                NationalCode = person.NationalCode,
            };

            return res;
            
        }
    }
}
