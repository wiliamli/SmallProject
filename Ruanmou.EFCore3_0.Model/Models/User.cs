namespace Ruanmou.EFCore3_0.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        //[StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Account { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Mobile { get; set; }

        //[ForeignKey("Company")]
        public int CompanyId { get; set; }

        [StringLength(500)]
        public string CompanyName { get; set; }

        public int State { get; set; }

        public int UserType { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public int? LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }

        public virtual Company Company { get; set; }
    }
}
