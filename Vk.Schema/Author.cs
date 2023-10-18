namespace Vk.Schema;

public class AuthorCreateRequest
{
    public int AuthorNumber { get; set; }           // Yazar Numarasi
    public string Name { get; set; }                // Yazar Adi
    public string Surname { get; set; }             // Yazar Soyadi
    public DateTime BirthDate { get; set; }         // Yazar Dogum Tarihi
}

public class AuthorUpdateRequest
{
    public string Name { get; set; }                // Yazar Adi
    public string Surname { get; set; }             // Yazar Soyadi
    public DateTime BirthDate { get; set; }         // Yazar Dogum Tarihi
}

public class AuthorResponse
{
    public int AuthorNumber { get; set; }           // Yazar Numarasi
    public string Name { get; set; }                // Yazar Adi
    public string Surname { get; set; }             // Yazar Soyadi
    public DateTime BirthDate { get; set; }         // Yazar Dogum Tarihi
    
    public virtual List<BookResponse> Books { get; set; }   // Bir yazarın birden fazla kitabı olabilir
}