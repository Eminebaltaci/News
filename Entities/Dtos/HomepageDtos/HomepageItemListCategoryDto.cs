using Core.Utilities.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.HomepageDtos;

public class HomepageItemListCategoryDto
{
    public Paginate<HomepageItemList> PaginatedNews { get; set; }
    public List<string> Categories { get; set; }
}
