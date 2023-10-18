namespace Vk.Schema;

public class CategoryCreateRequest
{
    public int CategoryNumber { get; set; }           // Kategori Numarasi
    public string CategoryName { get; set; }          // Kategori İsmi
}

public class CategoryUpdateRequest
{
    public string CategoryName { get; set; }          // Kategori İsmi
}

public class CategoryResponse
{
    public int CategoryNumber { get; set; }           // Kategori Numarasi
    public string CategoryName { get; set; }          // Kategori İsmi
    
    public virtual List<BookResponse> Books { get; set; }     // Bir kategorinin birden fazla kitabı kapsayabilir
}