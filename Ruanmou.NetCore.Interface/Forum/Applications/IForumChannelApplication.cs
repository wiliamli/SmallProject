using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumChannelApplication:IApplication
    {

        int AddForumChannel(ForumChannelDto forumChannelDto);

        void EditForumChannel(ForumChannelDto forumChannelDto);

        void DeleteForumChannel(int id);

        ForumChannelDto GetForumChannelById(int id);

        IEnumerable<ForumChannelDto> GetForumChannelByCreatedId(int createdId);

        IEnumerable<ForumChannelDto> GetForumChannelByRoleId(IList<int> roleIds);

        IEnumerable<ForumChannelDto> GetForumChannels();
    }
}
