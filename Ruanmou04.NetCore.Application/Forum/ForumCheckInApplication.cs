using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Application.Forum
{
    public class ForumCheckInApplication : IForumCheckInApplication
    {
        private IForumCheckInService forumCheckInService;

        public ForumCheckInApplication(IForumCheckInService forumCheckInService)
        {
            this.forumCheckInService = forumCheckInService;
        }

        public int AddCheckIn(ForumCheckInDto forumCheckInDto)
        {
            var forumCheckIn = forumCheckInService.Insert<ForumCheckIn>(forumCheckInDto.ToEntity());

            return forumCheckIn.Id;
        }

        public IEnumerable<ForumCheckInDto> getInfoByUserId(int userId)
        {
            return forumCheckInService.Query<ForumCheckIn>(m => m.UserId == userId).ToDtos();
        }
    }
}
