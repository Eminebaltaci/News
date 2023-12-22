using Entities.Dtos.ConfigurationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract;

public interface IConfigurationService
{
    TurkMedyaServiceEndpointsConfiguration GetTurkMedyaServiceEndpointsFromConfiguration();
}
