namespace ServiceShop.Api.Author.Model
{
    public class Degrees
    {
        public int DegreesId { get; set; }
        public string Name { get; set; }
        public string AcademicInstitution { get; set; }
        public DateTime Date { get; set; }
        public int BookAuthorId { get; set; }
        public BookAuthor BookAuthor { get; set; }
        public string DegreesGuid { get; set; }
    }
}
