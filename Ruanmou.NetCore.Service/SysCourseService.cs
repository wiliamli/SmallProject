using Microsoft.EntityFrameworkCore;
using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Interface;

namespace Ruanmou04.NetCore.Service
{
    public class SysCourseService : BaseService, ISysCourseService
    {
        public SysCourseService(DbContext context) : base(context)
        {

        } 


    }
}
