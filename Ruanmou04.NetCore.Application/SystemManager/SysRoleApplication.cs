using Ruanmou04.Core.Utility.Extensions;
using Ruanmou04.EFCore.Model.Models.SystemManager;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Input;
using Ruanmou04.NetCore.Dtos.SystemManager.RoleDtos.Output;
using Ruanmou04.NetCore.Interface.SystemManager.Applications;
using Ruanmou04.NetCore.Interface.SystemManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Ruanmou04.NetCore.Application.Extensions;
using Ruanmou04.Core.Utility.DtoUtilities;

namespace Ruanmou04.NetCore.Application.SystemManager
{
    public class SysRoleApplication : ISysRoleApplication
    {
        private ISysUsersRoleService sysRoleService;
        private ISysUserRoleMappingService sysUserRoleMappingService;

        public SysRoleApplication(ISysUsersRoleService sysRoleService,
            ISysUserRoleMappingService sysUserRoleMappingService)
        {
            this.sysRoleService = sysRoleService;
            this.sysUserRoleMappingService = sysUserRoleMappingService;
        }

        public IEnumerable<SysRoleDto> GetUserRoles(int userId)
        {
            IEnumerable<SysRoleDto> sysRoleDtos = null;
            try
            {
                var roleUser = sysUserRoleMappingService.Query<SysUserRoleMapping>(m => m.SysUserId == userId).ToList();

                var roles = sysRoleService.Query<SysRole>(m => m.Status).ToList();

                sysRoleDtos = (from a in roles
                               join b in roleUser on a.Id equals b.SysRoleId
                               select new SysRoleDto()
                               {
                                   Description = a.Description,
                                   Id = a.Id,
                                   Status = a.Status,
                                   Text = a.Text
                               }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sysRoleDtos;
        }

        /// <summary>
        /// 根据角色id删除角色
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRoleById(int id)
        {
            sysRoleService.Delete<SysRole>(id);
        }

        /// <summary>
        /// 根据角色Id获取角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SysRoleDto GetRoleDetailById(int roleId)
        {
            var roleDto = sysRoleService.Find<SysRole>(roleId).ToRoleDto();
            return roleDto;
        }

        /// <summary>
        /// 得到所有的角色
        /// </summary>
        /// <returns></returns>
        public List<SysRoleDto> GetAllRoles()
        {
            var roleDtos = sysRoleService.Query<SysRole>(u => u.Status).ToDtos();
            //.Select(m => new SysRoleDto
            //{
            //    Id = m.Id,
            //    Description = m.Description,
            //    Text = m.Text,

            //}).ToList();
            return roleDtos;
        }

        /// <summary>
        /// 分页获取角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagedResult<SysRoleListOutputDto> GetPagedResult(SysRoleListInputDto param)
        {
            if (param == null)
            {
                return null;
            }
            var name = param.Name;

            PagedResult<SysRole> pagedResult = sysRoleService.QueryPage<SysRole, int>((u => (!name.IsNullOrEmpty() && u.Text.Contains(name)) || name.IsNullOrEmpty()),
               param.PageSize,
               param.PageIndex, n => n.Id, false);

            return pagedResult.ToPaged();
        }

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="roleDto"></param>
        public void AddRole(SysRoleAddInputDto roleDto)
        {
            var roleEntity = roleDto.ToEntity();
            if (roleEntity == null)
            {
                return;
            }
            sysRoleService.Insert(roleEntity);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="roleEditDto"></param>
        public void EditRole(SysRoleEditInputDto roleEditDto)
        {
            if (roleEditDto == null || roleEditDto.Id <= 0)
            {
                return;
            }
            var sysRoleEntity = sysRoleService.Find<SysRole>(roleEditDto.Id);
            if (sysRoleEntity == null)
            {
                return;
            }

            sysRoleEntity.Description = roleEditDto.Description;
            sysRoleEntity.Status = roleEditDto.Status;
            sysRoleEntity.LastModifyTime = roleEditDto.LastModifyTime;
            sysRoleEntity.LastModifierId = roleEditDto.LastModifierId;
            sysRoleService.Update(sysRoleEntity);
        }

    }
}
