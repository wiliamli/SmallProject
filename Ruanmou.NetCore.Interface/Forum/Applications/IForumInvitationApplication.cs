using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumInvitationApplication
    {
        int AddForumInvitation(ForumInvitationDto forumInvitationDto);

        void UpdateForumInvitation(ForumInvitationDto forumInvitationDto);

        IEnumerable<ForumInvitationDto> GetForumInvitation(int topicId);
    }
}
