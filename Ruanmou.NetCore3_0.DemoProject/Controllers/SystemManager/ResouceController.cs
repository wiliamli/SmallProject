using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RM04.DBEntity;
using Ruanmou.NetCore.Interface;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Project.Models;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysResourceService _resourceService;

        public ResourceController(ICurrentUserInfo currentUserInfo, ISysResourceService resourceService)
        {
            _currentUserInfo = currentUserInfo;
            _resourceService = resourceService;
        }


        /// <summary>
        /// 获取编辑资源
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetEditResouceByID(int userId)
        {
            var user = _resourceService.Find<SysResource>(userId)?.MapTo<SysResource, SysResourceDto>();
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }


        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public string GetResouces(int page, int limit, string name)
        {
            var userData =
                (from r in _resourceService.
                Query<SysResource>(u => (!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty())
                 join ul in _resourceService.Query<SysUser>() on r.LastModifierId equals ul.Id into ul
                 from ulc in ul.DefaultIfEmpty()
                 join uc in _resourceService.Query<SysUser>() on r.CreatorId equals uc.Id into uc
                 from ucc in uc.DefaultIfEmpty()
                 select new SysResourceDto
                 {
                     Id = r.Id,
                     Name = r.Name,
                     Classes = r.Classes,
                     BrowseCount = r.BrowseCount,
                     Content = r.Content,
                     LastModifyTime = r.LastModifyTime,
                     LastModifier = ulc.Name,
                     Creator = ucc.Name
                 }).ToList();

            PagedResult<SysResourceDto> pagedResult = new PagedResult<SysResourceDto> { PageIndex = page, PageSize = limit, Rows = userData, Total = userData.Count };

            return JsonConvert.SerializeObject(pagedResult);


        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveData([FromBody]SysResourceInputDto sysMenuDto)
        {

            AjaxResult ajaxResult = new AjaxResult { success = false };
            if (sysMenuDto != null)
            {
                if (sysMenuDto.Id > 0)
                {
                    var menu = _resourceService.Find<SysResource>(sysMenuDto.Id);
                    menu.Id = sysMenuDto.Id;
                    menu.Name = sysMenuDto.Name;
                    menu.Classes = sysMenuDto.Classes;
                    menu.Content = sysMenuDto.Content;
                    menu.LastModifyTime = DateTime.Now;
                    menu.LastModifierId = _currentUserInfo.CurrentUser.Id;
                    _resourceService.Update<SysResource>(menu);
                }
                else
                {
                    var model = sysMenuDto.MapTo<SysResourceInputDto, SysResource>();
                    model.CreateTime = DateTime.Now;
                    model.CreatorId = _currentUserInfo.CurrentUser.Id;
                    _resourceService.Insert<SysResource>(model);
                }
                ajaxResult.msg = "保存成功";
                ajaxResult.success = true;
            }
            else
                ajaxResult.msg = "保存失败";
            return ajaxResult;
        }
    }
}
