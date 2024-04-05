using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GrupoArchicentroWebAppTest.Models;

namespace GrupoArchicentroWebAppTest.Data
{
    public class GrupoArchicentroWebAppTestContext : DbContext
    {
        public GrupoArchicentroWebAppTestContext (DbContextOptions<GrupoArchicentroWebAppTestContext> options)
            : base(options)
        {
        }

        public DbSet<GrupoArchicentroWebAppTest.Models.Empleado> Empleado { get; set; } = default!;
    }
}
