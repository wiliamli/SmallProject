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
    public class RoleController : ControllerBase
    {
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ISysUsersRoleService _sysRoleService;

        public RoleController(ICurrentUserInfo currentUserInfo, ISysUsersRoleService sysRoleService)
        {
            _currentUserInfo = currentUserInfo;
            _sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 获取编辑用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public string GetEditRoleByID(int userId)
        {
            var user = _sysRoleService.Find<SysRole>(userId)?.MapTo<SysRole, SysRoleDto>();
            return JsonConvert.SerializeObject(new AjaxResult { success = true, data = user });

        }
        [HttpGet]
        public string GetUserSetRoles()
        {
            var userData = _sysRoleService.
               Query<SysRole>(u => u.Status)
               .Select(m => new SysRoleDto
               {
                   Id = m.Id,
                   Description = m.Description,
                   Text = m.Text,

               }).ToList();
            return JsonConvert.SerializeObject(userData);
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public string GetRoles(int page, int limit, string name)
        {
            var userData = _sysRoleService.
                Query<SysRole>(u => (!name.IsNullOrEmpty() && u.Text.Contains(name)) || name.IsNullOrEmpty())
                .Select(m => new SysRoleDto
                {
                    Id = m.Id,
                    Description = m.Description,
                    CreateTime = m.CreateTime,
                    Text = m.Text,

                    Status = m.Status,

                }).ToList();

            PagedResult<SysRoleDto> pagedResult = new PagedResult<SysRoleDto> { PageIndex = page, PageSize = limit, Rows = userData, Total = userData.Count };

            return JsonConvert.SerializeObject(pagedResult);


        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="sysMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult SaveData([FromBody]SysRoleDto sysMenuDto)
        {

            AjaxResult ajaxResult = new AjaxResult { success = false };
            if (sysMenuDto != null)
            {
                if (sysMenuDto.Id > 0)
                {
                    var model = _sysRoleService.Find<SysRole>(sysMenuDto.Id);
                    model.Id = sysMenuDto.Id;
                    model.Description = sysMenuDto.Description;
                    model.Status = sysMenuDto.Status;
                    model.LastModifyTime = DateTime.Now;
                    model.LastModifierId = _currentUserInfo.CurrentUser.Id;
                    _sysRoleService.Update<SysRole>(model);
                }
                else
                {
                    var model = sysMenuDto.MapTo<SysRoleDto, SysRole>();
                    model.CreateTime = DateTime.Now;
                    model.CreateId= _currentUserInfo.CurrentUser.Id;
                    _sysRoleService.Insert<SysRole>(model);
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
