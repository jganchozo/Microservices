using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceShop.Api.Book.Application;

namespace ServiceShop.Api.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialLibraryDto>>> GetBooks()
        {
            return await _mediator.Send(new SelectAll.Execute());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialLibraryDto>> GetBook(Guid id)
        {
            return await _mediator.Send(new FilterSelect.Execute() { BookId = id });
        }
    }
}
