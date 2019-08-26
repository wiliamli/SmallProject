namespace Ruanmou.EFCore3_0.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Category")]
    public partial class Category
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(100)]
        public string ParentCode { get; set; }

        public int? CategoryLevel { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Url { get; set; }

        public int? State { get; set; }
    }
}
