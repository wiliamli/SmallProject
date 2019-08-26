using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RM04.DBEntity
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
