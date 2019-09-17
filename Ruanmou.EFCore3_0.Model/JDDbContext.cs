namespace Ruanmou.EFCore3_0.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using RM04.DBEntity;
    using Ruanmou.Core.Utility;
    using Ruanmou.EFCore3_0.Model.SqlLog;
    using Ruanmou04.EFCore.Model.Models.Forum;

    /// <summary>
    /// 日志问题
    /// </summary>
    public partial class JDDbContext : DbContext
    {
        private IConfiguration _IConfiguration = null;
        public JDDbContext(IConfiguration configuration)
        {
            this._IConfiguration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var builder = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json");
            // var configuration = builder.Build();
            // var conn = configuration.GetConnectionString("JDDbConnection");

            //optionsBuilder.UseSqlServer(this._IConfiguration.GetConnectionString("JDDbConnectionString"));


            optionsBuilder.UseLoggerFactory(new CustomEFLoggerFactory());

            optionsBuilder.UseSqlServer(StaticConstraint.JDDbConnection);

            //optionsBuilder.UseSqlServer("Server=.;Database=advanced11;User id=sa;password=Passw0rd");

        }

        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysResource> SysResource { get; set; }
        public virtual DbSet<SysMenuOperation> SysMenuOperation { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRoleMenuMapping> SysRoleMenuMapping { get; set; }

        public virtual DbSet<SysRoleMenuOperationMapping> SysRoleMenuOperationMapping { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUserMenuMapping> SysUserMenuMapping { get; set; }
        public virtual DbSet<SysUserMenuOperationMapping> SysUserMenuOperationMapping { get; set; }
        public virtual DbSet<SysUserRoleMapping> SysUserRoleMapping { get; set; }

        public virtual DbSet<SysCourse> SysCourse { get; set; }
        public virtual DbSet<SysCourseCategory> SysCourseCategory { get; set; }


        public virtual DbSet<ForumAttachment> ForumAttachment { get; set; }
        public virtual DbSet<ForumChannel> ForumChannel { get; set; }
        public virtual DbSet<ForumCheckIn> ForumCheckIn { get; set; }
        public virtual DbSet<ForumConcern> ForumConcern { get; set; }
        public virtual DbSet<ForumInvitation> ForumInvitation { get; set; }
        public virtual DbSet<ForumPersonal> ForumPersonal { get; set; }
        public virtual DbSet<ForumRoleChannel> ForumRoleChannel { get; set; }
        public virtual DbSet<ForumTopic> ForumTopic { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.SourcePath)
                .IsUnicode(false);
            modelBuilder.Entity<SysMenu>()
                .Property(e => e.Status)
                .HasDefaultValue(1);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.WeChat)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Status)
                .HasDefaultValue(1);

            modelBuilder.Entity<SysRole>()
                .Property(e => e.Status)
                .HasDefaultValue(1);
        }
    }
}
