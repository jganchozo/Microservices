using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceShop.Api.Author.Application;

namespace ServiceShop.Api.Author.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            return await _mediator.Send(new Select.AuthorList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(string id)
        {
            return await _mediator.Send(new FilterSelect.UniqueAuthor() { BookAuthorGuid = id });
        }
    }
}
