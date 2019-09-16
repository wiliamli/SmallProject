using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Project.Models;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumInvitationController : BaseApiController
    {
        private IForumInvitationApplication forumInvitationApplication;
        private IMemoryCache memoryCache;
        private ICurrentUserInfo currentUserInfo;

        public ForumInvitationController(IForumInvitationApplication forumInvitationApplication,
            IMemoryCache memoryCache, ICurrentUserInfo currentUserInfo) : base(memoryCache, currentUserInfo)
        {
            this.forumInvitationApplication = forumInvitationApplication;
        }


        // GET: api/ForumInvitation
        [ServiceFilter(typeof(VerifyAttribute))]
        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumInvitationDto>> GetInvitations(int topicId)
        {
            return StandardAction(()=> forumInvitationApplication.GetForumInvitation(topicId));
        }

        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumInvitationDto>> GetInvitationsByUserId(int userId)
        {
            return StandardAction(() => forumInvitationApplication.GetForumInvitationByUserId(userId));
        }

        /// <summary>
        /// 只看楼主
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StandardJsonResult<ForumInvitationDto> GetOnlyInvitation(int id)
        {
            return StandardAction(() => forumInvitationApplication.GetOnlyForumInvitation(id));
        }

        // POST: api/ForumInvitation
        [HttpPost]
        public StandardJsonResult<int> Add(ForumInvitationDto forumInvitationDto)
        {
            return StandardAction(() => forumInvitationApplication.AddForumInvitation(forumInvitationDto));
        }

        // PUT: api/ForumInvitation/5
        [HttpPost]
        public StandardJsonResult Edit(ForumInvitationDto forumInvitationDto)
        {
            return StandardAction(() => forumInvitationApplication.UpdateForumInvitation(forumInvitationDto));
        }

        // DELETE: api/ApiWithActions/5
        [HttpGet]
        public StandardJsonResult Delete(int id)
        {
            return StandardAction(() => forumInvitationApplication.DeleteForumInvitation(id));
        }
    }
}
