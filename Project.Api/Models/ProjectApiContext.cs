using Microsoft.EntityFrameworkCore;
using Project.Api.Models;

namespace Project.Api.Models
{
    public class ProjectApiContext : DbContext
    {
        public ProjectApiContext(DbContextOptions<ProjectApiContext> option): base(option)
        {

        }
        public DbSet<Project.Api.Models.User> Users { get; set; } = default!;
    }
}
