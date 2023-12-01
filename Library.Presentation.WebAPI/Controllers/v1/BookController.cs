using Library.Core.Application.Dtos.Book;
using Library.Core.Application.Features.Book.Commands.AddBookCommand;
using Library.Core.Application.Features.Book.Commands.DeleteBookCommand;
using Library.Core.Application.Features.Book.Commands.UpdateBookCommand;
using Library.Core.Application.Features.Book.Queries.GetAllBook;
using Library.Core.Application.Features.Book.Queries.GetBookById;
using Library.Presentation.WebApi.Controllers;
using Library.Presentation.WebApi.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Presentation.WebAPI.Controllers.v1
{
    [SwaggerTag("Mantenimiento de Libros")]
    public class BookController : BaseApiController
    {
        public BookController()
        {

        }

        [HttpGet("get-all")]
        [SwaggerOperation(
           Summary = "Obtener todos los libros.",
            Description = "Nos permite obtener todos los libros."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var result = await Mediator.Send(new GetAllBookQuery());

                if(result == null || result.Count  == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        [SwaggerOperation(
           Summary = "Obtener un libro.",
            Description = "Nos permite obtener un libro por su id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(string id)
        {
            try
            {
                var result = await Mediator.Send(new GetBookByIdQuery() { Id = id});

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [Authorize]
        [HttpPost("add")]
        [SwaggerOperation(
           Summary = "Registra un libro.",
            Description = "Al especificar las propiedades nos permite llevar acabo el registro de un libro."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBook(AddBookCommand command)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await Mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update")]
        [SwaggerOperation(
           Summary = "Actualiza un libro.",
            Description = "Al especificar las propiedades nos permite llevar acabo la actualización de un libro."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook(UpdateBookCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await Mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        [SwaggerOperation(
           Summary = "Elimina un libro.",
            Description = "Nos permite eliminar un libro por su id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteBookCommand() { Id = id});

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
