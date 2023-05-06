using ProjectApi.Data.Models.Data;
using ProjectApi.Data.Models.IRepositories;
using ProjectApi.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApi.Data.Models.Repositories
{
    public class PersonRepository : GenericRepository<Person> , IPersonRepository
    {
        public PersonRepository(ProjectApiContext context) : base(context)
        {

        }
    }
}
