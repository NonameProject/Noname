using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentList.Models
{
    public class StudentListContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Mark> Marks { get; set; }

        public DbSet<User> Users { get; set; }

    }
}