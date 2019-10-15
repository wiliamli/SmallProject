using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility;
using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Dtos.DtoHelper;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.NetCore.Project.Controllers;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using System.Collections.Generic;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Output;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class RoleController : BaseApiController //ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysRoleApplication _sysRoleApplication;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentUserInfo"></param> 
        public RoleController(ICurrentUserInfo currentUserInfo, ISysRoleApplication sysRoleApplication) : base(currentUserInfo)
        {
            _currentUserInfo = currentUserInfo;
            _sysRoleApplication = sysRoleApplication;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult DeleteRoleById(int id)
        {
            return StandardAction(() => _sysRoleApplication.DeleteRoleById(id));
        }

        /// <summary>
        /// 获取编辑角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<SysRoleDto> GetEditRoleById(int id)
        {
            return StandardAction(() => _sysRoleApplication.GetRoleDetailById(id));
        }

        /// <summary>
        /// 得到所有的角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<List<SysRoleDto>> GetUserSetRoles()
        {
            //var userData = _sysRoleService.
            //   Query<SysRole>(u => u.Status)
            //   .Select(m => new SysRoleDto
            //   {
            //       Id = m.Id,
            //       Description = m.Description,
            //       Text = m.Text,

            //   }).ToList();
            //return JsonConvert.SerializeObject(userData);

            return StandardAction(() => _sysRoleApplication.GetAllRoles());
        }

        /// <summary>
        /// 分页获取所有角色数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<PagedResult<SysRoleListOutputDto>> GetRoles(int page, int limit, string name)
        {
            var param = new SysRoleListInputDto()
            {
                Name = name,
                PageIndex = page,
                PageSize = limit
            };
            return StandardAction(() => _sysRoleApplication.GetPagedResult(param));

            //var userData = _sysRoleService.
            //    Query<SysRole>(u => (!name.IsNullOrEmpty() && u.Text.Contains(name)) || name.IsNullOrEmpty())
            //    .Select(m => new SysRoleDto
            //    {
            //        Id = m.Id,
            //        Description = m.Description,
            //        CreateTime = m.CreateTime,
            //        Text = m.Text,

            //        Status = m.Status,

            //    }).ToList();

            //PagedResult<SysRoleDto> pagedResult = new PagedResult<SysRoleDto> { PageIndex = page, PageSize = limit, Rows = userData, Total = userData.Count };
            //return JsonConvert.SerializeObject(pagedResult);
        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysRoleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult SaveData([FromBody]SysRoleDto sysRoleDto)
        {

            if (sysRoleDto == null)
            {
                return StandardJsonResult.GetFailureResult("参数有误");
            }
            if (sysRoleDto.Id > 0)
            {
                var sysRoleEditInputDto = DataMapping<SysRoleDto, SysRoleEditInputDto>.Trans(sysRoleDto);
                sysRoleEditInputDto.LastModifierId = _currentUserInfo.SysCurrentUser.Id;
                return StandardAction(() => _sysRoleApplication.EditRole(sysRoleEditInputDto));
            }
            else
            {
                var sysRoleAddInputDto = DataMapping<SysRoleDto, SysRoleAddInputDto>.Trans(sysRoleDto);
                sysRoleAddInputDto.CreateId = _currentUserInfo.SysCurrentUser.Id;
                return StandardAction(() => _sysRoleApplication.AddRole(sysRoleAddInputDto));
            }

            //AjaxResult ajaxResult = new AjaxResult { success = false };
            //if (sysMenuDto != null)
            //{
            //    if (sysMenuDto.Id > 0)
            //    {
            //        var model = _sysRoleService.Find<SysRole>(sysMenuDto.Id);
            //        model.Id = sysMenuDto.Id;
            //        model.Description = sysMenuDto.Description;
            //        model.Status = sysMenuDto.Status;
            //        model.LastModifyTime = DateTime.Now;
            //        model.LastModifierId = _currentUserInfo.CurrentUser.Id;
            //        _sysRoleService.Update<SysRole>(model);
            //    }
            //    else
            //    {
            //        var model = sysMenuDto.MapTo<SysRoleDto, SysRole>();
            //        model.CreateTime = DateTime.Now;
            //        model.CreateId = _currentUserInfo.CurrentUser.Id;
            //        _sysRoleService.Insert<SysRole>(model);
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
