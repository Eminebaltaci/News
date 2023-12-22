using Business.BusinessConstants;
using Core.Utilities.Dynamic;
using Core.Utilities.Results;
using Entities.Dtos;
using Entities.Dtos.DetailpageDtos;
using Entities.Dtos.HomepageDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract;

public interface ITurkMedyaClientService
{
    Task<IDataResult<List<HomepageItemList>>> GetAllHomepageNewsAsync();
    Task<IDataResult<DetailpageNews>> GetNewsDetailsAsync();
    Task<List<SelectListItem>> GetNewsCategoryListAsync();
    Task<IDataResult<Paginate<HomepageItemList>>> GetAllPaginatedHomepageNewsAsync(int? index , int? size);
    Task<IDataResult<Paginate<HomepageItemList>>> GetFilteredHomepageNewsAsync(string category, string searchText, int? index, int? size);
}
