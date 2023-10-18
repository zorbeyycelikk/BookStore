namespace Vk.Schema;

public class BookCreateRequest
{
    public int BookNumber { get; set; }            // KitapId
    public string HeadLine { get; set; }           // Baslik
    public int PageCount { get; set; }             // Sayfa Sayisi
    public string Publisher { get; set; }          // Yayin Evi
    public string ISNB { get; set; }               // Uluslararası Standart Kitap Numarasi
    public int AuthorId { get; set; }              // Foreign Key From AuthorTable
    public int CategoryId { get; set; }            // Foreign Key From CategoryTable
}

public class BookUpdateRequest
{
    public int BookNumber { get; set; }            // KitapId
    public int PageCount { get; set; }             // Sayfa Sayisi
    public string Publisher { get; set; }          // Yayin Evi
}

public class BookResponse
{
    public int BookNumber { get; set; }            // KitapId
    public string HeadLine { get; set; }           // Baslik
    public int PageCount { get; set; }             // Sayfa Sayisi
    public string Publisher { get; set; }          // Yayin Evi
    public string ISNB { get; set; }               // Uluslararası Standart Kitap Numarasi
    
    public int AuthorName { get; set; }              // Foreign Key From AuthorTable
    public int CategoryName { get; set; }            // Foreign Key From CategoryTable
}