using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.HomepageDtos;

public class HomepageNews
{
    public int ErrorCode { get; set; }
    public object ErrorMessage { get; set; }
    public List<HomepageData> Data { get; set; }
}
