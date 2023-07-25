using ServiceShop.Api.Author.Model;

namespace ServiceShop.Api.Author.Application
{
    public class AuthorDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BookAuthorGuid { get; set; }
    }
}
