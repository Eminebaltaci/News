using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ConfigurationDtos;

public class TurkMedyaServiceEndpointsConfiguration
{
    public string Domain { get; set; }
    public string GetHomePageNewsEndpointPath { get; set; }
    public string GetNewsDetailPageEndpointPath { get; set; }
}
