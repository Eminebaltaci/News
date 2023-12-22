using Core.Utilities.Dynamic;

namespace Core.CoreExtensions;
/**
    Bu sınıf, bir IList koleksiyonunu sayfalar halinde bölerek sayfalama yapmayı sağlar.
    ToPaginate metodu, kaynağı belirtilen sayfa indeksi ve boyutu dikkate alarak parçalar.
    Kaynak listesinin uzunluğu alınır ve istenen sayfa indeksi ve boyuta göre parçalanmış bir liste oluşturulur.
    Oluşturulan sayfalama sonucu, Paginate yapısında saklanır ve döndürülür.
 **/
public static class IListPaginateExtensions
{
    public static Paginate<T> ToPaginate<T>(
            this IList<T> source,
            int index,
            int size
        )
    {
        int count = source.Count();

        List<T> items = source.Skip(index * size)
            .Take(size)
            .ToList();

        Paginate<T> list = new()
        {
            Index = index,
            TotalCount = count,
            Items = items,
            Pages = (int)Math.Ceiling(count / (double)size),
            Size = size
        };
        return list;
    }
}
