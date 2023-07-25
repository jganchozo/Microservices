namespace ServiceShop.Api.Author.Model
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public ICollection<Degrees> DegreeList { get; set; }
        public string BookAuthorGuid { get; set; }
    }
}