using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentService.Models;

namespace StudentService.Data
{
    public class StudentServiceContext : DbContext
    {
        public StudentServiceContext (DbContextOptions<StudentServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Stud> Stud { get; set; } = default!;
    }
}
