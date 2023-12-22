namespace Entities.Dtos.HomepageDtos;

public class HomepageData
{
    public string SectionType { get; set; }
    public string RepeatType { get; set; }
    public int ItemCountInRow { get; set; }
    public bool LazyLoadingEnabled { get; set; }
    public bool TitleVisible { get; set; }
    public string Title { get; set; }
    public object TitleColor { get; set; }
    public string TitleBgColor { get; set; }
    public string SectionBgColor { get; set; }
    public List<HomepageItemList> Itemlist { get; set; }
    public int TotalRecords { get; set; }
}
