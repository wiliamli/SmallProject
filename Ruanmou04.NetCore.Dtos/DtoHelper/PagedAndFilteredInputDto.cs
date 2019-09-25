using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Ruanmou.Core.Utility;

namespace Ruanmou04.EFCore.Dtos.DtoHelper
{
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {
        [Range(1, StaticConstraint.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public string Filter { get; set; }

		
		//// custom codes 
		
        //// custom codes end


        public PagedAndFilteredInputDto()
        {
            MaxResultCount = StaticConstraint.DefaultPageSize;
        }
    }
}