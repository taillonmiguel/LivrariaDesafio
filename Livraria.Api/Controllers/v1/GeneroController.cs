using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.ViewModels;
using Livraria.Shared.Controllers;
using Livraria.Shared.Data;
using Livraria.Shared.DomainValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Api.Controllers.v1
{
    [ApiController]
    [Route("Genero/v1")]
    [Produces("application/json")]
    public class GeneroController : BaseApiController<GeneroController>
    {
        private readonly IGeneroService _generoService;
        private readonly IDomainValidation _domainValidation;


        public GeneroController(IDomainValidation domainValidation,
                                ILogger<GeneroController> logger,
                                IUnityOfWork unityOfWork,
                                IGeneroService generoService)
            : base(domainValidation, unityOfWork)
        {
            _generoService = generoService;
            _domainValidation = domainValidation;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneroViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ExecuteAsync(async () =>
            {
                var response = await _generoService.GetByIdAsync(id);

                if (response == null)
                    _domainValidation.Add("Gênero não encontrado.");

                return Ok(response);
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GeneroViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            return await ExecuteAsync(async () =>
            {
                var response = await _generoService.GetAllAsync();
                return Ok(response);
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneroViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDomainValidation))]
        public async Task<IActionResult> Post([FromBody] GeneroDto request)
        {
            return await ExecuteWithTransactionAsync(async () =>
            {
                var response = await _generoService.CreateAsync(request);
                return Ok(response);
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneroViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDomainValidation))]
        public async Task<IActionResult> Put(Guid id, [FromBody] GeneroDto request)
        {
            return await ExecuteWithTransactionAsync(async () =>
            {
                var response = await _generoService.UpdateAsync(id, request);
                return Ok(response);
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDomainValidation))]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await ExecuteWithTransactionAsync(async () =>
            {
                await _generoService.DeleteAsync(id);
                return Ok();
            });
        }
    }
}
