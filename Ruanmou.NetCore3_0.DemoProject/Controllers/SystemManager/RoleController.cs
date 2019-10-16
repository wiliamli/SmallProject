using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Output;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Project.Controllers;
using System.Collections.Generic;

namespace Ruanmou.NetCore3_0.DemoProject.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [CustomAuthorize]
    [Route("api/[controller]/[action]"), ApiController]
    public class RoleController : BaseApiController
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
        }
    }
}
