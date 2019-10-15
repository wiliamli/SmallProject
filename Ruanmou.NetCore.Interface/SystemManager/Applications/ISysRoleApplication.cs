using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Output;
using Ruanmou04.NetCore.Interface;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Interface.SystemManager.Applications
{
    public interface ISysRoleApplication : IApplication
    {
        IEnumerable<SysRoleDto> GetUserRoles(int userId);

        /// <summary>
        /// 根据角色id删除角色
        /// </summary>
        /// <param name="id"></param>
        void DeleteRoleById(int id);

        /// <summary>
        /// 根据角色Id获取角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        SysRoleDto GetRoleDetailById(int roleId);

        /// <summary>
        /// 得到所有的角色
        /// </summary>
        /// <returns></returns>
        public List<SysRoleDto> GetAllRoles();


        /// <summary>
        /// 分页获取角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResult<SysRoleListOutputDto> GetPagedResult(SysRoleListInputDto param);

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="roleDto"></param>
        public void AddRole(SysRoleAddInputDto roleDto);

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="roleEditDto"></param>
        public void EditRole(SysRoleEditInputDto roleEditDto);

    }
}
