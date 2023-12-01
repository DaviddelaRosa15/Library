using Library.Core.Application.Dtos.Author;
using Library.Core.Application.Features.Author.Commands.AddAuthorCommand;
using Library.Core.Application.Features.Author.Commands.DeleteAuthorCommand;
using Library.Core.Application.Features.Author.Commands.UpdateAuthorCommand;
using Library.Core.Application.Features.Author.Queries.GetAllAuthor;
using Library.Core.Application.Features.Author.Queries.GetAuthorById;
using Library.Presentation.WebApi.Controllers;
using Library.Presentation.WebApi.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Presentation.WebAPI.Controllers.v1
{
    [SwaggerTag("Mantenimiento de Autores")]
    public class AuthorController : BaseApiController
    {
        public AuthorController()
        {

        }

        [HttpGet("get-all")]
        [SwaggerOperation(
           Summary = "Obtener todos los autores.",
            Description = "Nos permite obtener todos los autores."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuthorDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                var result = await Mediator.Send(new GetAllAuthorQuery());

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
           Summary = "Obtener un autor.",
            Description = "Nos permite obtener un autor por su id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthor(string id)
        {
            try
            {
                var result = await Mediator.Send(new GetAuthorByIdQuery() { Id = id});

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
           Summary = "Registra un autor.",
            Description = "Al especificar las propiedades nos permite llevar acabo el registro de un autor."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAuthor(AddAuthorCommand command)
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
        [HttpPatch("update")]
        [SwaggerOperation(
           Summary = "Actualiza un autor.",
            Description = "Al especificar las propiedades nos permite llevar acabo la actualización de un libro."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
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
           Summary = "Elimina un autor.",
            Description = "Nos permite eliminar un autor por su id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteAuthorCommand() { Id = id});

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
