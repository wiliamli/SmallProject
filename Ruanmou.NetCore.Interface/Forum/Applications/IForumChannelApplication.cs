using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumChannelApplication:IApplication
    {

        public int AddForumChannel(ForumChannelDto forumChannelDto);

        public void EditForumChannel(ForumChannelDto forumChannelDto);

        public void DeleteForumChannel(int id);

        public ForumChannelDto GetForumChannelById(int id);
        public IEnumerable<ForumChannelDto> GetAllForumChannel();
        public IEnumerable<ForumChannelDto> GetForumChannelByCreatedId(int createdId);

        public IEnumerable<ForumChannelDto> GetForumChannelByRoleId(int roleId);
    }
}
