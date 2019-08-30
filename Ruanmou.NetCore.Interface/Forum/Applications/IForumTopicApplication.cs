using Ruanmou04.Core.Utility;
using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using System.Collections.Generic;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumTopicApplication:IApplication
    {
        int AddTopic(ForumTopicDto forumTopicDto);

        void UpdateTopic(ForumTopicDto forumTopicDto);

        Core.Utility.PagedResult<ForumTopicDto> GetPagedResult(int channelId,PagingInput pagingInput);

        ForumTopicDto GetForumTopic(int topicId);
    }
}
