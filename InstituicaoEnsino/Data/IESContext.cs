using InstituicaoEnsino.Models;
using Microsoft.EntityFrameworkCore;

namespace InstituicaoEnsino.Data
{
    public class IESContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }

        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {
            
        }

    }
}
