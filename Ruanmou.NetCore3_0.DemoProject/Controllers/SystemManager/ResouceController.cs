using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;  
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Application.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.ResourceDtos.Output;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.NetCore.Project.Controllers;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 资源管理
    /// </summary>
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class ResourceController :BaseApiController // ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        //private readonly ISysResourceService _resourceService;
        private readonly ISysResourceApplication _sysResourceApplication;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUserInfo"></param>
        /// <param name="sysResourceApplication"></param>
        public ResourceController(ICurrentUserInfo currentUserInfo, ISysResourceApplication sysResourceApplication):base(currentUserInfo)
        {
            _currentUserInfo = currentUserInfo;
            _sysResourceApplication = sysResourceApplication;
        }


        /// <summary>
        /// 获取编辑资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult GetEditResouceById(int id)
        {
            //var user = _resourceService.Find<SysResource>(userId)?.MapTo<SysResource, SysResourceDto>();
            //return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });
            return StandardAction(() => _sysResourceApplication.GetResourceById(id));
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<PagedResult<SysResourceListOutputDto>> GetResouces(int page, int limit, string name)
        {
            var param = new SysResourceListInputDto()
            {
                Name = name,
                PageIndex = page,
                PageSize = limit
            };
            return StandardAction(() => _sysResourceApplication.GetPagedResult(param));

            //var userData =
            //    (from r in _resourceService.
            //    Query<SysResource>(u => (!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty())
            //     join ul in _resourceService.Query<SysUser>() on r.LastModifierId equals ul.Id into ul
            //     from ulc in ul.DefaultIfEmpty()
            //     join uc in _resourceService.Query<SysUser>() on r.CreatorId equals uc.Id into uc
            //     from ucc in uc.DefaultIfEmpty()
            //     select new SysResourceDto
            //     {
            //         Id = r.Id,
            //         Name = r.Name,
            //         Classes = r.Classes,
            //         BrowseCount = r.BrowseCount,
            //         Content = r.Content,
            //         LastModifyTime = r.LastModifyTime,
            //         LastModifier = ulc.Name,
            //         Creator = ucc.Name
            //     }).ToList();

            //PagedResult<SysResourceDto> pagedResult = new PagedResult<SysResourceDto> { PageIndex = page, PageSize = limit, Rows = userData, Total = userData.Count };

            //return JsonConvert.SerializeObject(pagedResult);


        }


        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysResourceDto"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult SaveData([FromBody]SysResourceInputDto sysResourceDto)
        {
            if (sysResourceDto == null)
            {
                return StandardJsonResult.GetFailureResult("参数有误");
            }
            if (sysResourceDto.Id > 0)
            {
                var sysResourceEditInputDto = DataMapping<SysResourceInputDto, SysResourceEditInputDto>.Trans(sysResourceDto);
                sysResourceEditInputDto.LastModifierId = _currentUserInfo.SysCurrentUser.Id;
                return StandardAction(() => _sysResourceApplication.EditResource(sysResourceEditInputDto));
            }
            else
            {
                var sysResourceAddInputDto = DataMapping<SysResourceInputDto, SysResourceAddInputDto>.Trans(sysResourceDto);
                sysResourceAddInputDto.CreatorId = _currentUserInfo.SysCurrentUser.Id;
                return StandardAction(() => _sysResourceApplication.AddResource(sysResourceAddInputDto));
            }

            //AjaxResult ajaxResult = new AjaxResult { success = false };
            //if (sysMenuDto != null)
            //{
            //    if (sysMenuDto.Id > 0)
            //    {
            //        var menu = _resourceService.Find<SysResource>(sysMenuDto.Id);
            //        menu.Id = sysMenuDto.Id;
            //        menu.Name = sysMenuDto.Name;
            //        menu.Classes = sysMenuDto.Classes;
            //        menu.Content = sysMenuDto.Content;
            //        menu.LastModifyTime = DateTime.Now;
            //        menu.LastModifierId = _currentUserInfo.CurrentUser.Id;
            //        _resourceService.Update<SysResource>(menu);
            //    }
            //    else
            //    {
            //        var model = sysMenuDto.MapTo<SysResourceInputDto, SysResource>();
            //        model.CreateTime = DateTime.Now;
            //        model.CreatorId = _currentUserInfo.CurrentUser.Id;
            //        _resourceService.Insert<SysResource>(model);
            //    }
            //    ajaxResult.msg = "保存成功";
            //    ajaxResult.success = true;
            //}
            //else
            //    ajaxResult.msg = "保存失败";
            //return ajaxResult;
        }
    }
}
