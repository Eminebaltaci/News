using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.DetailpageDtos;


public class DetailpageNews
{
    public int ErrorCode { get; set; }
    public object ErrorMessage { get; set; }
    public DetailpageData Data { get; set; }
}




