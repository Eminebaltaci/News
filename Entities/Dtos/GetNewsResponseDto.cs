using Core.Utilities.Dynamic;
using Entities.Dtos.HomepageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos;

public class GetNewsResponseDto
{
    public Paginate<HomepageItemList>? News { get; set; } = new Paginate<HomepageItemList>();
}
