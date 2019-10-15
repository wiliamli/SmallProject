using Microsoft.EntityFrameworkCore.Storage;
using Ruanmou04.EFCore.Dtos.ForumDtos;
using Ruanmou04.EFCore.Model.Models.Forum;
using Ruanmou04.NetCore.Interface.Forum.Applications;
using Ruanmou04.NetCore.Interface.Forum.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Ruanmou04.Core.Dtos.DtoHelper;
using Ruanmou04.Core.Utility.DtoUtilities;
using Ruanmou04.Core.Utility.Extensions;
using System.Data;

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
            var entity=forumChannelService.Find<ForumChannel>(id);
            if (entity==null)
            {
                return;
            }
            entity.Status = false;
            forumChannelService.Update<ForumChannel>(entity);
            //System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1] 
            //{new System.Data.SqlClient.SqlParameter("@id",SqlDbType.Int,4){ Value=id } };
            //forumChannelService.Excute<ForumChannel>($"UPDATE ForumChannel SET Status=0 WHERE Id=@id", paramList);
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
        public IEnumerable<ForumChannelDto> GetAllForumChannel()
        {
            return forumChannelService.Query< ForumChannel>(fc=>fc.Status).Select(fc=>fc.MapTo<ForumChannel, ForumChannelDto>());
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

        public PagedResult<ForumChannelDto> GetPagedResult(string name, PagingInput pagingInput)
        {
            var pagedResult = forumTopicService.QueryPage<ForumChannel, DateTime>
                (u => ((!name.IsNullOrEmpty() && u.Name.Contains(name)) || name.IsNullOrEmpty()), pagingInput.PageSize,
                pagingInput.PageIndex, n => n.ModifiedDate.Value, false);

            return pagedResult.ToPaged();
        }
    }
}
