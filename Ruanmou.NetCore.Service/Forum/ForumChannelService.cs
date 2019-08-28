using Microsoft.EntityFrameworkCore;
using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Interface.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Service.Forum
{
    public class ForumChannelService : BaseService, IForumChannelService
    {
        public ForumChannelService(DbContext context) : base(context)
        {
        }
    }
}
