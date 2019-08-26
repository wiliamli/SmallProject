namespace Ruanmou.EFCore3_0.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Company")]
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public int? LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
