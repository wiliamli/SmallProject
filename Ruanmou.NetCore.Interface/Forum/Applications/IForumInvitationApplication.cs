using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumInvitationApplication : IApplication
    {
        int AddForumInvitation(ForumInvitationDto forumInvitationDto);

        void UpdateForumInvitation(ForumInvitationDto forumInvitationDto);


        IEnumerable<ForumInvitationDto> GetForumInvitation(int topicId);


        IEnumerable<ForumInvitationDto> GetForumInvitationByUserId(int userId);

        ForumInvitationDto GetOnlyForumInvitation(int id);

        void DeleteForumInvitation(int id);
    }
}
