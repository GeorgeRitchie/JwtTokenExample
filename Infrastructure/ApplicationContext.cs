using EskhataDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EskhataDigital
{
    // add-migration Inin -o "Repository/Migrations"

    /// <summary>
    /// Контекст для доступа к БД
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ExperienceLevel> ExperienceLevels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<UserResume> UserResumes { get; set; }
        public DbSet<VacancyCategory> VacancyCategories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserAuthenticationData> UserAuthenticationDatas { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
