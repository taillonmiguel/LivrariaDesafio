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
    [Route("Livro/v1")]
    [Produces("application/json")]
    public class LivroController : BaseApiController<LivroController>
    {
        private readonly ILivroService _livroService;
        private readonly IDomainValidation _domainValidation;

        public LivroController(IDomainValidation domainValidation,
                               ILogger<LivroController> logger,
                               IUnityOfWork unityOfWork,
                               ILivroService livroService)
            : base(domainValidation, unityOfWork)
        {
            _livroService = livroService;
            _domainValidation = domainValidation;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LivroViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ExecuteAsync(async () =>
            {
                var response = await _livroService.GetByIdAsync(id);

                if (response == null)
                    _domainValidation.Add("Livro não encontrado.");

                return Ok(response);
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LivroViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            return await ExecuteAsync(async () =>
            {
                var response = await _livroService.GetAllAsync();
                return Ok(response);
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LivroViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDomainValidation))]
        public async Task<IActionResult> Post([FromBody] LivroDto request)
        {
            return await ExecuteWithTransactionAsync(async () =>
            {
                var response = await _livroService.CreateAsync(request);
                return Ok(response);
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LivroViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDomainValidation))]
        public async Task<IActionResult> Put(Guid id, [FromBody] LivroDto request)
        {
            return await ExecuteWithTransactionAsync(async () =>
            {
                var response = await _livroService.UpdateAsync(id, request);
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
                await _livroService.DeleteAsync(id);
                return Ok();
            });
        }
    }
}
