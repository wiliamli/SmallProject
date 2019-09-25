using Ruanmou04.EFCore.Dtos.ForumDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumCheckInApplication:IApplication
    {

        int AddCheckIn(ForumCheckInDto forumCheckDto);

        IEnumerable<ForumCheckInDto> getInfoByUserId(int userId);

    }
}
