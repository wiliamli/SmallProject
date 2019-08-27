using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpAspNetCoreDemo.Model
{
    public interface IHasTen
    {
        Guid TenantId { get; set; }
    }
}
