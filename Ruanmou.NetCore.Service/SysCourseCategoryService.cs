using Microsoft.EntityFrameworkCore;
using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Interface;

namespace Ruanmou04.NetCore.Service
{
    public class SysCourseCategoryService : BaseService, ISysCourseCategoryService
    {
        public SysCourseCategoryService(DbContext context) : base(context)
        {

        } 


    }
}
