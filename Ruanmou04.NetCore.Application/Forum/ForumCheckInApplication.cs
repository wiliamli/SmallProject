using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
            int result = 0;

            IEnumerable<ForumCheckIn> forumCheckIns = forumCheckInService.Query<ForumCheckIn>(m => m.UserId == forumCheckInDto.UserId).ToList();
            forumCheckIns = forumCheckIns.Where(m=>DateTime.Now.IsSameDay(m.CheckDate.Value));

            if (forumCheckIns.Count() == 0)
            {
                var forumCheckIn = forumCheckInService.Insert<ForumCheckIn>(forumCheckInDto.ToEntity());

                result = forumCheckIn.Id;
            }

            return result;
        }

        public IEnumerable<ForumCheckInDto> getInfoByUserId(int userId)
        {
            return forumCheckInService.Query<ForumCheckIn>(m => m.UserId == userId).ToDtos();
        }
    }
}
