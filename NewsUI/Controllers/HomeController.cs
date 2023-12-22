using Business.Abstract;
using Core.Utilities.Dynamic;
using Entities.Dtos;
using Entities.Dtos.HomepageDtos;
using Microsoft.AspNetCore.Mvc;
using NewsUI.Models;
using System.Diagnostics;

namespace NewsUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITurkMedyaClientService _turkMedyaClientService;

        public HomeController(ILogger<HomeController> logger, ITurkMedyaClientService turkMedyaClientService)
        {
            _logger = logger;
            _turkMedyaClientService = turkMedyaClientService;
        }
        /** İstenilen sayfa parametrelerine (category, searchText, page) göre haberleri filtreler ve sayfalama yapar.
         Eğer category veya searchText parametrelerinden herhangi biri boş değilse, 
         filtrelenmiş haberleri GetFilteredHomepageNewsAsync metodunu kullanarak alır ve sayfaya gönderir.
         Eğer category ve searchText parametreleri boş ise, tüm sayfalı ana sayfa haberlerini GetAllPaginatedHomepageNewsAsync metodunu kullanarak alır ve sayfaya gönderir.
         Elde edilen haberler ve sayfa bilgileri ViewBag üzerinden viewe aktarılır.
         Ayrıca, kategori listesi GetNewsCategoryListAsync metodu kullanılarak ViewBag üzerinden görünüme aktarılır. **/
        public async Task<IActionResult> Index(string? category, string? searchText, int page = 1)
        {
            GetNewsResponseDto response = new();
            if (!string.IsNullOrEmpty(category) || !string.IsNullOrEmpty(searchText))
            {
                var filteredNews = await _turkMedyaClientService.GetFilteredHomepageNewsAsync(category, searchText, index: page - 1, null);
                response.News = filteredNews?.Data;
            }
            else
            {
                var news = await _turkMedyaClientService.GetAllPaginatedHomepageNewsAsync(page - 1, null);
                response.News = news?.Data;
            }
            if (response != null && response?.News?.Items?.Count > 0)
            {
                ViewBag.CurrentPage = response.News.Index + 1;
                ViewBag.PageSize = response.News.Pages;
                ViewBag.TotalItemCount = response.News.TotalCount;
                ViewBag.Category = category;
                ViewBag.SearchText = searchText;
            }
            ViewBag.SelectedCategory = category;
            ViewBag.SearchText = searchText;
            var categoryList = await _turkMedyaClientService.GetNewsCategoryListAsync();
            ViewBag.Categories = categoryList;
            return View(response);
        }
        /** Bu metot, bir haberin detaylarını alma işlevini kontrol eder.
         _turkMedyaClientService üzerinden GetNewsDetailsAsync metodunu kullanarak haber detaylarını alır.
         Aldığı haber detaylarını, haberin görünümüne (View) aktarır.**/
        public async Task<IActionResult> Details()
        {
            var newsDetails = await _turkMedyaClientService.GetNewsDetailsAsync();
            return View(newsDetails?.Data);
        }
        /** Bu metot, kullanıcıdan gelen kategori ve arama metni bilgisini alarak, 
         Index metoduyla yeniden yönlendirme (redirect) işlemi gerçekleştirir.
         Kullanıcının girdiği kategori ve arama metni, Index metoduna yönlendirilir 
         ve bu parametreler Index metoduna aktarılır, Index metodu çalıştırılır.
         Bu işlem, kullanıcının arama yaparken URL'de parametreleri göstermek yerine 
         Index sayfasına yönlendirme yaparak kullanıcı deneyimini iyileştirmek için kullanılır. **/
        [HttpGet]
        public async Task<IActionResult> Search(string category, string searchText)
        {
            return RedirectToAction(nameof(Index), new
            {
                category = category,
                searchText = searchText
            });
        }
    }
}
