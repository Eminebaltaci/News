using Business.Abstract;
using Business.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business;

public static class BusinessServiceRegistration
{
    /** Bu metot, business katmanı servislerinin kaydını IServiceCollection'a ekler.
     IConfigurationService ve ITurkMedyaClientService servislerini ekler.
     IConfigurationService, ConfigurationManager sınıfı için tekil bir örnek olarak kaydedilir.
     ITurkMedyaClientService, TurkMedyaClientManager sınıfı için HttpClient tabanlı bir istemci olarak kaydedilir.**/
    public static IServiceCollection AddBusinessServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfigurationService, ConfigurationManager>();
        services.AddHttpClient<ITurkMedyaClientService, TurkMedyaClientManager>();
        return services;
    }
}
