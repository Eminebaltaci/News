using Business.Abstract;
using Business.BusinessConstants;
using Core.CoreExtensions;
using Core.Utilities.Dynamic;
using Core.Utilities.Results;
using Entities.Dtos;
using Entities.Dtos.DetailpageDtos;
using Entities.Dtos.HomepageDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concrete;

public class TurkMedyaClientManager : ITurkMedyaClientService
{
    private readonly HttpClient _httpClient;
    private readonly IConfigurationService _configurationService;
    public TurkMedyaClientManager(HttpClient httpClient, IConfigurationService configurationService)
    {
        _httpClient = httpClient;
        _configurationService = configurationService;
    }
    /** Bu metod, TurkMedya servisinden ana sayfa haberlerini almak için bir istek gönderir.
        Configuration servisinden TurkMedya servisi endpoint bilgilerini alır.
        Eğer bilgiler başarıyla alınırsa, istek URL'si oluşturulur ve bu URL üzerinden istek yapılır.
        Gelen yanıtın başarılı olup olmadığı kontrol edilir ve başarılı ise sadece haber listesi döndürülür.
        Eğer bir hata oluşursa, ilgili hata mesajıyla birlikte hata sonucu döndürülür.**/
    public async Task<IDataResult<List<HomepageItemList>>> GetAllHomepageNewsAsync()
    {
        var endpointConfigurations = _configurationService.GetTurkMedyaServiceEndpointsFromConfiguration();
        if (endpointConfigurations == null)
            return new ErrorDataResult<List<HomepageItemList>>(ConstantMessages.TurkMedyaEndpointNullException);
        string requestUrl = $"{endpointConfigurations.Domain}{endpointConfigurations.GetHomePageNewsEndpointPath}";
        var response = await MakeRequestAsync<HomepageNews>(requestUrl);
        if (!response.Success)
            return new ErrorDataResult<List<HomepageItemList>>(response.Message);
        var allNewsItems = response?.Data?.Data?.Where(item => item.SectionType == ConstantValues.SectionTypeNews).FirstOrDefault()?.Itemlist;
        return new SuccessDataResult<List<HomepageItemList>>(allNewsItems);
    }
    /** Bu metot, tüm ana sayfa haberlerini sayfalı bir liste olarak döndürür.
        Tüm ana sayfa haberlerini almak için GetAllHomepageNewsAsync metodunu kullanır.
        Alınan tüm haberler, istenilen sayfa indeksi ve boyutuna göre parçalanır (sayfalama yapılır).
        Sayfalama işlemi sonucunda elde edilen haberler, Paginate yapısı içinde başarılı bir şekilde döndürülür.**/
    public async Task<IDataResult<Paginate<HomepageItemList>>> GetAllPaginatedHomepageNewsAsync(int? index, int? size)
    {
        int pageIndex = index ?? ConstantValues.DefaultIndex;
        int pageSize = size ?? ConstantValues.DefaultSize;
        var allNews = await GetAllHomepageNewsAsync();
        return new SuccessDataResult<Paginate<HomepageItemList>>(
            allNews?.Data.ToPaginate(pageIndex, pageSize)
            );
    }
    /** Bu metot, ana sayfa haberlerini kategoriye, metin aramasına ve sayfalama ölçütlerine göre filtreleyerek döndürür.
     İstenen kategoriye göre filtreleme yapar ve ayrıca verilen metin aramasıyla başlık eşleşmelerini kontrol eder.
     Haberler, sayfalama için verilen indeks ve boyut bilgilerine göre parçalanır (sayfalama yapılır).
     Eğer filtre sonucunda haberler bulunursa, bu haberlerin sayfalanmış versiyonu başarıyla döndürülür.
     Haberler bulunamazsa veya filtrelerle eşleşen sonuç yoksa, ilgili hata sonucu döndürülür.**/
    public async Task<IDataResult<Paginate<HomepageItemList>>> GetFilteredHomepageNewsAsync(string category, string searchText, int? index, int? size)
    {
        int pageIndex = index ?? ConstantValues.DefaultIndex;
        int pageSize = size ?? ConstantValues.DefaultSize;
        var newsResult = await GetAllHomepageNewsAsync();
        if (!newsResult.Success) return new ErrorDataResult<Paginate<HomepageItemList>>(newsResult.Message);
        var news = newsResult.Data;
        if (!string.IsNullOrEmpty(category))
            news = news.Where(n => n.Category.Title == category).ToList();
        if (!string.IsNullOrEmpty(searchText))
            news = news.Where(n => n.Title.ToLower().Contains(searchText.ToLower())).ToList();
        if (news.Count > 0)
            return new SuccessDataResult<Paginate<HomepageItemList>>(news.ToPaginate(pageIndex, pageSize));
        return new ErrorDataResult<Paginate<HomepageItemList>>();
    }
    /** Bu metot, tüm ana sayfa haberlerinin kategorilerinden benzersiz olanları bir SelectListItem listesi olarak döndürür.
     Öncelikle GetAllHomepageNewsAsync metodu ile tüm ana sayfa haberleri alınır.
     Ardından, kategorilerden benzersiz olanları bir SelectListItem listesi olarak oluşturur.
     Sonuç olarak, her bir kategori için bir SelectListItem nesnesi oluşturulur ve bu nesneler listesi döndürülür.**/
    public async Task<List<SelectListItem>> GetNewsCategoryListAsync()
    {
        var allNews = await GetAllHomepageNewsAsync();
        var result = new List<SelectListItem>() { new SelectListItem { Text = ConstantValues.SelectCategory, Value = string.Empty } };
        allNews?.Data.Select(item => item.Category?.Title).Where(title => !string.IsNullOrEmpty(title)).Distinct().ToList().ForEach(title =>
        {
            result.Add(new SelectListItem { Text = title, Value = title });
        });
        return result;
    }
    /** Bu metod, TurkMedya servisinden bir haberin detaylarını almak için istek gönderir.
     Eğer yapılandırma bilgileri eksikse veya istek başarısız olursa, ilgili hata sonucu döndürülür.
     Başarılı bir yanıt alındığında, DetailpageNews tipindeki veri başarılı bir şekilde döndürülür.**/
    public async Task<IDataResult<DetailpageNews>> GetNewsDetailsAsync()
    {
        var endpointConfigurations = _configurationService.GetTurkMedyaServiceEndpointsFromConfiguration();
        if (endpointConfigurations == null)
            return new ErrorDataResult<DetailpageNews>(ConstantMessages.TurkMedyaEndpointNullException);
        string requestUrl = $"{endpointConfigurations.Domain}{endpointConfigurations.GetNewsDetailPageEndpointPath}";
        var response = await MakeRequestAsync<DetailpageNews>(requestUrl);
        if (!response.Success)
            return new ErrorDataResult<DetailpageNews>(response.Message);
        return new SuccessDataResult<DetailpageNews>(
            response?.Data
            );
    }
    /** Bu metot, belirtilen URL üzerinden HTTP GET isteği yaparak veri alır.
     Aldığı URL'ye gönderdiği isteğin başarılı olup olmadığını kontrol eder.
     Başarılı bir yanıt alındığında, JSON içeriğini belirtilen tipe dönüştürür ve başarılı bir sonuç döndürür.
     Eğer istek başarısız olursa veya boş bir yanıt gelirse, hata sonucu döndürülür.**/
    private async Task<IDataResult<T>> MakeRequestAsync<T>(string requestUrl) where T : class
    {
        var result = await _httpClient.GetAsync(requestUrl);
        if (!result.IsSuccessStatusCode)
            return new ErrorDataResult<T>();
        var strResponse = await result.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(strResponse))
            return new ErrorDataResult<T>();
        var response = JsonConvert.DeserializeObject<T>(strResponse);
        return new SuccessDataResult<T>(response);
    }
}
