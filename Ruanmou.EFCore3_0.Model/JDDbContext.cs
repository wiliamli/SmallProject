namespace Ruanmou.EFCore3_0.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
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

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<JDCommodity> JD_Commodity_001 { get; set; }
        public virtual DbSet<JD_Commodity_002> JD_Commodity_002 { get; set; }
        public virtual DbSet<JD_Commodity_003> JD_Commodity_003 { get; set; }
        public virtual DbSet<JD_Commodity_004> JD_Commodity_004 { get; set; }
        public virtual DbSet<JD_Commodity_005> JD_Commodity_005 { get; set; }
        public virtual DbSet<JD_Commodity_006> JD_Commodity_006 { get; set; }
        public virtual DbSet<JD_Commodity_007> JD_Commodity_007 { get; set; }
        public virtual DbSet<JD_Commodity_008> JD_Commodity_008 { get; set; }
        public virtual DbSet<JD_Commodity_009> JD_Commodity_009 { get; set; }
        public virtual DbSet<JD_Commodity_010> JD_Commodity_010 { get; set; }
        public virtual DbSet<JD_Commodity_011> JD_Commodity_011 { get; set; }
        public virtual DbSet<JD_Commodity_012> JD_Commodity_012 { get; set; }
        public virtual DbSet<JD_Commodity_013> JD_Commodity_013 { get; set; }
        public virtual DbSet<JD_Commodity_014> JD_Commodity_014 { get; set; }
        public virtual DbSet<JD_Commodity_015> JD_Commodity_015 { get; set; }
        public virtual DbSet<JD_Commodity_016> JD_Commodity_016 { get; set; }
        public virtual DbSet<JD_Commodity_017> JD_Commodity_017 { get; set; }
        public virtual DbSet<JD_Commodity_018> JD_Commodity_018 { get; set; }
        public virtual DbSet<JD_Commodity_019> JD_Commodity_019 { get; set; }
        public virtual DbSet<JD_Commodity_020> JD_Commodity_020 { get; set; }
        public virtual DbSet<JD_Commodity_021> JD_Commodity_021 { get; set; }
        public virtual DbSet<JD_Commodity_022> JD_Commodity_022 { get; set; }
        public virtual DbSet<JD_Commodity_023> JD_Commodity_023 { get; set; }
        public virtual DbSet<JD_Commodity_024> JD_Commodity_024 { get; set; }
        public virtual DbSet<JD_Commodity_025> JD_Commodity_025 { get; set; }
        public virtual DbSet<JD_Commodity_026> JD_Commodity_026 { get; set; }
        public virtual DbSet<JD_Commodity_027> JD_Commodity_027 { get; set; }
        public virtual DbSet<JD_Commodity_028> JD_Commodity_028 { get; set; }
        public virtual DbSet<JD_Commodity_029> JD_Commodity_029 { get; set; }
        public virtual DbSet<JD_Commodity_030> JD_Commodity_030 { get; set; }
        public virtual DbSet<SysLog> SysLogs { get; set; }
        public virtual DbSet<SysMenu> SysMenus { get; set; }
        public virtual DbSet<SysRole> SysRoles { get; set; }
        public virtual DbSet<SysRoleMenuMapping> SysRoleMenuMappings { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUserMenuMapping> SysUserMenuMappings { get; set; }
        public virtual DbSet<SysUserRoleMapping> SysUserRoleMappings { get; set; }
        public virtual DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ParentCode)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JDCommodity>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JDCommodity>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_002>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_002>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_003>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_003>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_004>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_004>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_005>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_005>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_006>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_006>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_007>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_007>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_008>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_008>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_009>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_009>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_010>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_010>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_011>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_011>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_012>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_012>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_013>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_013>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_014>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_014>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_015>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_015>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_016>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_016>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_017>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_017>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_018>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_018>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_019>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_019>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_020>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_020>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_021>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_021>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_022>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_022>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_023>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_023>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_024>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_024>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_025>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_025>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_026>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_026>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_027>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_027>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_028>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_028>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_029>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_029>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_030>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<JD_Commodity_030>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

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

            modelBuilder.Entity<User>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Mobile)
                .IsUnicode(false);
        }
    }
}
