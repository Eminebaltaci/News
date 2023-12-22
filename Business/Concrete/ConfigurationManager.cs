using Business.Abstract;
using Entities.Dtos.ConfigurationDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class ConfigurationManager : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /** Bu metod, json dosyasındaki "ServiceEndpoints" bölümünden "TurkMedya" altındaki
     değerleri alarak, TurkMedya servisi için ilgili alan adı (Domain),
     ana sayfa haberleri endpoint'i (GetHomePageNewsEndpointPath) ve
     haber detayları endpoint'i (GetNewsDetailPageEndpointPath) içeren bir yapılandırma sınıfını döndürür.
     Bu yapılandırma, TurkMedya servisiyle iletişim kurmak için gerekli endpoint bilgilerini içerir.**/
    public TurkMedyaServiceEndpointsConfiguration GetTurkMedyaServiceEndpointsFromConfiguration()
    {
        return _configuration.GetSection("ServiceEndpoints:TurkMedya")
            .Get<TurkMedyaServiceEndpointsConfiguration>();
    }
}
