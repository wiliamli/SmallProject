using Microsoft.AspNetCore.Mvc;
using Ruanmou04.Core.Utility.MvcResult;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Project.Controllers.Forum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ForumInvitationController : BaseApiController
    {
        private IForumInvitationApplication forumInvitationApplication;

        public ForumInvitationController(IForumInvitationApplication forumInvitationApplication)
        {
            this.forumInvitationApplication = forumInvitationApplication;
        }


        // GET: api/ForumInvitation
        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumInvitationDto>> GetInvitations(int topicId)
        {
            return StandardAction(()=> forumInvitationApplication.GetForumInvitation(topicId));
        }

        [HttpGet]
        public StandardJsonResult<IEnumerable<ForumInvitationDto>> GetInvitationsbyUserId(int userId)
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
