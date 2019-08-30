using Ruanmou04.EFCore.Model.Dtos.ForumDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou04.NetCore.Interface.Forum.Applications
{
    public interface IForumConcernApplication:IApplication
    {
        int AddConcern(ForumConcernDto forumConcernDto);

        void DeleteConcern(int id);


    }
}
