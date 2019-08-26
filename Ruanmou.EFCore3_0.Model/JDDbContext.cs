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

   
    

        public virtual DbSet<SysMenu> SysMenus { get; set; }
        public virtual DbSet<SysMenuOperation> SysMenuOperation { get; set; }
        public virtual DbSet<SysRole> SysRoles { get; set; }
        public virtual DbSet<SysRoleMenuMapping> SysRoleMenuMappings { get; set; }

        public virtual DbSet<SysRoleMenuOperationMapping> SysRoleMenuOperationMapping { get; set; }

        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUserMenuMapping> SysUserMenuMappings { get; set; }
        public virtual DbSet<SysUserMenuOperationMapping> SysUserMenuOperationMapping { get; set; }
        public virtual DbSet<SysUserRoleMapping> SysUserRoleMappings { get; set; }



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

            
        }
    }
}
