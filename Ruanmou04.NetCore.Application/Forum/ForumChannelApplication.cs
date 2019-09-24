using Microsoft.EntityFrameworkCore.Storage;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Ruanmou04.NetCore.Application.Forum
{
    public class ForumChannelApplication: IForumChannelApplication
    {
        private IForumChannelService forumChannelService;

        private IForumRoleChannelService forumRoleChannelService;

        private IForumTopicService forumTopicService;

        public ForumChannelApplication(IForumChannelService forumChannelService,
            IForumRoleChannelService forumRoleChannelService,
            IForumTopicService forumTopicService)
        {
            this.forumChannelService = forumChannelService;
            this.forumRoleChannelService = forumRoleChannelService;
            this.forumTopicService = forumTopicService;
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
            System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1] 
            {new System.Data.SqlClient.SqlParameter("@id",id)};
            forumChannelService.Excute<ForumChannel>($"UPDATE ForumChannel SET Status=0 WHERE Id=@id", paramList);
        }

        public void EditForumChannel(ForumChannelDto forumChannelDto)
        {
            forumChannelService.Update<ForumChannel>(forumChannelDto.ToEntity());
        }

        public IEnumerable<ForumChannelDto> GetForumChannelByCreatedId(int createdId)
        {
            return forumChannelService.Query<ForumChannel>(m => m.CreatedId == createdId).ToDtos();
        }

        public ForumChannelDto GetForumChannelById(int id)
        {
            return forumChannelService.Find<ForumChannel>(id).ToDto();
        }

        public IEnumerable<ForumChannelDto> GetForumChannelByRoleId(IList<int> roleIds)
        {
            // dbContext 没有单例
            // sql 脚本执行提示异常
            // TODO：
            var forumChannels = forumChannelService.Query<ForumChannel>(null).ToList();

            var forumRoleChannels = forumRoleChannelService.Query<ForumRoleChannel>(m => roleIds.Contains(m.SysRoleId)).ToList();

            var forumTopics = forumTopicService.Query<ForumTopic>(m => m.Status).ToList();

            var query = (from a in forumChannels
                         join b in forumRoleChannels on a.Id equals b.ChannelId
                         select a).ToDtos().ToList();
            query.ForEach(m=>
            {
                m.ForumTopics = forumTopics.Where(n => n.ChannelId == m.Id).ToDtos();
            });

            return query;
;
        }

        public IEnumerable<ForumChannelDto> GetForumChannels()
        {
            var forumChannels = forumChannelService.Query<ForumChannel>(null).ToList();
            var forumTopics = forumTopicService.Query<ForumTopic>(m => m.Status).ToList();

            var query = forumChannels.ToDtos().ToList();

            query.ForEach(m =>
            {
                m.ForumTopics = forumTopics.Where(n => n.ChannelId == m.Id).ToDtos();
            });

            return query;
        }
    }
}
