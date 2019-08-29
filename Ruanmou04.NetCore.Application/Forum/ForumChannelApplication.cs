using Microsoft.EntityFrameworkCore.Storage;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Application.Forum
{
    public class ForumChannelApplication: IForumChannelApplication
    {
        private IForumChannelService forumChannelService;

        public ForumChannelApplication(IForumChannelService forumChannelService)
        {
            this.forumChannelService = forumChannelService;
        }


        public int AddForumChannel(ForumChannelDto forumChannelDto)
        {
            ForumChannel forumChannel = null;
            using (IDbContextTransaction transaction = forumChannelService.Context.Database.BeginTransaction())
            {
                try
                {
                    forumChannel = forumChannelService.Insert(forumChannelDto.ToEntity());
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return forumChannel != null ? forumChannel.Id : 0;
        }

        public void DeleteForumChannel(int id)
        {
            throw new NotImplementedException();
        }

        public void EditForumChannel(ForumChannelDto forumChannelDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ForumChannelDto> GetForumAttachmentByCreatedId(int createdId)
        {
            throw new NotImplementedException();
        }

        public ForumChannelDto GetForumChannelById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
