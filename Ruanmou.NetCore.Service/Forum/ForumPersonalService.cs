using Microsoft.EntityFrameworkCore;
using Ruanmou.NetCore.Service;
using Ruanmou04.NetCore.Interface.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Service.Forum
{
    public class ForumPersonalService : BaseService, IForumPersonalService
    {
        public ForumPersonalService(DbContext context) : base(context)
        {
        }
    }
}
