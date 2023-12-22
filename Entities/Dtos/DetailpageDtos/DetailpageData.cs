namespace Entities.Dtos.DetailpageDtos;

public class DetailpageData
{
    public DetailpageHeaderAd HeaderAd { get; set; }
    public DetailpageNewsDetail NewsDetail { get; set; }
    public DetailpageFooterAd FooterAd { get; set; }
    public DetailpageMultimedia Multimedia { get; set; }
    public List<DetailpageItemList> ItemList { get; set; }
    public DetailpageRelatedNews RelatedNews { get; set; }
    public DetailpageVideo Video { get; set; }
    public DetailpagePhotoGallery PhotoGallery { get; set; }
}




