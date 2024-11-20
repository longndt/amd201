using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeacherService.Models;

namespace TeacherService.Data
{
    public class TeacherServiceContext : DbContext
    {
        public TeacherServiceContext (DbContextOptions<TeacherServiceContext> options)
            : base(options)
        {
        }

        public DbSet<TeacherService.Models.Teacher> Teacher { get; set; } = default!;
    }
}
