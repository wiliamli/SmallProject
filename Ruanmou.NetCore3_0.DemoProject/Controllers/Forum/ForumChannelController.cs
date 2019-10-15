using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.NetCore.AOP.Filter;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.Forum.Applications; 

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]    
    [ApiController]
    public class ForumChannelController : BaseApiController
    {
        private IForumChannelApplication forumChannelApplication;
        private ICurrentUserInfo currentUserInfo;

        public ForumChannelController(IForumChannelApplication forumChannelApplication,ICurrentUserInfo currentUserInfo) :base(currentUserInfo)
        {
            this.forumChannelApplication = forumChannelApplication;
        }


        /// <summary>
        /// 根据角色获取所有频道
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(VerifyAttribute))]
        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumChannelDto>> GetChannelsByRoleId(IList<int> roleIds)
        {
            return StandardAction(() => forumChannelApplication.GetForumChannelByRoleId(roleIds));
        }


        /// <summary>
        /// 获取所有频道
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(VerifyAttribute))]
        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumChannelDto>> GetChannels()
        {
            return StandardAction(() => forumChannelApplication.GetForumChannels());
        }

        [CustomAuthorizeAttribute]
        [HttpGet]
        public StandardJsonResult<PagedResult<ForumChannelDto>> GetPagedChannels(int page, int limit, string name)
        {
            var param = new PagingInput()
            {
                PageSize = limit,
                PageIndex = page ,
            };
            var result = StandardAction(() => forumChannelApplication.GetPagedResult(name,param));
            return result;
        }

        // GET: api/ForumChannel/5
        [CustomAuthorizeAttribute]       
        [HttpGet("{id}", Name = "Get")]
        public StandardJsonResult<ForumChannelDto> GetChannel(int id)
        {
            return StandardAction(() => forumChannelApplication.GetForumChannelById(id));
        }

        // POST: api/ForumChannel
        [CustomAuthorizeAttribute]
        [HttpPost]
        public StandardJsonResult<int> AddChannel(ForumChannelDto forumChannelDto)
        {
            return StandardAction(() => forumChannelApplication.AddForumChannel(forumChannelDto));
        }

        // PUT: api/ForumChannel/5
        [CustomAuthorizeAttribute]
        [HttpPost]
        public StandardJsonResult EditChannel(ForumChannelDto forumChannelDto)
        {
            return StandardAction(() => forumChannelApplication.EditForumChannel(forumChannelDto));
        }

        // DELETE: api/ApiWithActions/5
        [CustomAuthorizeAttribute]
        //[HttpDelete("{id}")]
        [HttpGet] //前台统一使用
        public StandardJsonResult Delete(int id)
        {
            return StandardAction(() => forumChannelApplication.DeleteForumChannel(id));
        }
    }
}
