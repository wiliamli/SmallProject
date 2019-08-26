
using Microsoft.EntityFrameworkCore;
using Ruanmou.NetCore.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.Service
{
    public class CommodityService : BaseService, ICommodityService
    {
        public CommodityService(DbContext context) : base(context)
        {
        }
    }
}
