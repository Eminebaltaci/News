using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Dynamic;
/** Paginate sınıfı, sayfalama işlemi sonucunda elde edilen verilerin ve sayfa bilgilerinin tutulduğu yapıyı temsil eder.
 Bu sınıf, sayfalama işlemi için gerekli bilgileri barındırır: sayfa boyutu (Size), sayfa indeksi (Index), 
 toplam veri sayısı, toplam sayfa sayısı, sayfalanan öğelerin listesi gibi.
 Ayrıca, bir önceki ve bir sonraki sayfa olup olmadığını belirlemek için HasPrevious ve HasNext özellikleri bulunur. **/
public class Paginate<T>
{
    public int Size { get; set; }
    public int Index { get; set; }
    public int TotalCount { get; set; }
    public int Pages { get; set; }
    public IList<T> Items { get; set; }
    public bool HasPrevious => Index > 0;
    public bool HasNext => Index + 1 < Pages;
    public Paginate()
    {
        Items = Array.Empty<T>();
    }
}
