using System.Collections.Generic;
using crude_class_library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace crude_class_library
{
    public class IBDatabaseDbContext : DbContext
    {
        public IBDatabaseDbContext()
        {

        }
        public IBDatabaseDbContext(DbContextOptions<IBDatabaseDbContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<DepartmentAdmin> DepartmentAdmin { get; set; }
        public virtual DbSet<ATRCreators> ATRCreators { get; set; } = default!;
        public virtual DbSet<BoardCommittees> BoardCommittees { get; set; }
        public virtual DbSet<Committees> Committees { get; set; }
        public virtual DbSet<Designations> Designations { get; set; }
        public virtual DbSet<SuperAdmin> SuperAdmin { get; set; }
        public virtual DbSet<EdakApproversMaster> EdakApproversMaster { get; set; }
        public virtual DbSet<EmailTemplates> EmailTemplates { get; set; }
        public virtual DbSet<NoteApproversMaster> NoteApproversMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=IBDatabase");
            }
        }

    }
}
