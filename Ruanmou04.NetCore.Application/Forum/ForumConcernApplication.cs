using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Application.Forum
{
    public class ForumConcernApplication : IForumConcernApplication
    {
        private IForumConcernService forumConcernService;

        public ForumConcernApplication(IForumConcernService forumConcernService) {

            this.forumConcernService = forumConcernService;
        }

        public int AddConcern(ForumConcernDto forumConcernDto)
        {
            var forumConcern = forumConcernService.Insert<ForumConcern>(forumConcernDto.ToEntity());
            return forumConcern.Id;
        }

        public void DeleteConcern(int id)
        {
            forumConcernService.Delete<ForumConcern>(id);
        }
    }
}
