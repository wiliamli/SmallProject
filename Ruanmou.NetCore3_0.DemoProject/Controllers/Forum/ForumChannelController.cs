using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumChannelController : BaseApiController
    {
        private IForumChannelApplication forumChannelApplication;

        public ForumChannelController(IForumChannelApplication forumChannelApplication)
        {
            this.forumChannelApplication = forumChannelApplication;
        }


        /// <summary>
        /// 根据角色获取所有频道
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumChannelDto>> GetChannelsByRoleId(int roleId)
        {
            return StandardAction(() => forumChannelApplication.GetForumChannelByRoleId(roleId));
        }

        // GET: api/ForumChannel/5
        [HttpGet("{id}", Name = "Get")]
        public StandardJsonResult<ForumChannelDto> GetChannel(int id)
        {
            return StandardAction(() => forumChannelApplication.GetForumChannelById(id));
        }

        // POST: api/ForumChannel
        [HttpPost]
        public StandardJsonResult<int> AddChannel(ForumChannelDto forumChannelDto)
        {
            return StandardAction(() => forumChannelApplication.AddForumChannel(forumChannelDto));
        }

        // PUT: api/ForumChannel/5
        [HttpPost]
        public StandardJsonResult EditChannel(ForumChannelDto forumChannelDto)
        {
            return StandardAction(() => forumChannelApplication.EditForumChannel(forumChannelDto));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public StandardJsonResult Delete(int id)
        {
            return StandardAction(() => forumChannelApplication.DeleteForumChannel(id));
        }
    }
}
