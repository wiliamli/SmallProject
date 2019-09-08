using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruanmou04.NetCore.Application.Forum
{
    public class ForumInvitationApplication : IForumInvitationApplication
    {
        private IForumInvitationService forumInvitationService;

        public ForumInvitationApplication(IForumInvitationService forumInvitationService)
        {
            this.forumInvitationService = forumInvitationService;
        }

        public int AddForumInvitation(ForumInvitationDto forumInvitationDto)
        {
            var forumInvitation = forumInvitationService.Insert<ForumInvitation>(forumInvitationDto.ToEntity());
            return forumInvitation.Id;
        }

        public void DeleteForumInvitation(int id)
        {
            forumInvitationService.Delete<ForumInvitation>(id);
        }

        public IEnumerable<ForumInvitationDto> GetForumInvitation(int topicId)
        {
            var forumInvitations = forumInvitationService.Query<ForumInvitation>(m=>m.TopicId==topicId);

            //ForumInvitationDto forumInvitationDto = forumInvitations.FirstOrDefault(m => m.ParantId == 0).ToDto();

            //GetInvitations(forumInvitationDto, forumInvitations.ToDtos());

            return forumInvitations.ToDtos();
        }

        public ForumInvitationDto GetOnlyForumInvitation(int id)
        {
            return forumInvitationService.Find<ForumInvitation>(id).ToDto();
        }

        public void UpdateForumInvitation(ForumInvitationDto forumInvitationDto)
        {
            forumInvitationService.Update<ForumInvitation>(forumInvitationDto.ToEntity());
        }

        private void GetInvitations(ForumInvitationDto parant, IEnumerable<ForumInvitationDto> forumInvitationDtos)
        {
            //
            var childInvitations = forumInvitationDtos.Where(m => m.ParentId == parant.Id);
            //parant.ChildInvitation.AddRange(childInvitations);

            foreach (var item in childInvitations)
            {
                GetInvitations(item, childInvitations);
            }
        }
    }
}
