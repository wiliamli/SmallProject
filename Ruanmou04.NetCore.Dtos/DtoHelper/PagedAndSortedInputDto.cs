using Abp.Application.Services.Dto;
using Ruanmou.Core.Utility;

namespace Ruanmou04.EFCore.Dtos.DtoHelper
{
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }

		
		//// custom codes 
		
        //// custom codes end

        public PagedAndSortedInputDto()
        {
            MaxResultCount = StaticConstraint.DefaultPageSize;
        }
    }
}