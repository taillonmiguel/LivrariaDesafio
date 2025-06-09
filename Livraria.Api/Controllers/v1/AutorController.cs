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
    [Route("Autor/v1")]
    [Produces("application/json")]
    [Authorize]
    public class AutorController : BaseApiController<AutorController>
    {
        private readonly IAutorService _autorService;
        private readonly IDomainValidation _domainValidation;

        public AutorController(IDomainValidation domainValidation,
                               IUnityOfWork unityOfWork,
                               IAutorService autorService)
            : base(domainValidation, unityOfWork)
        {
            _autorService = autorService;
            _domainValidation = domainValidation;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutorViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ExecuteAsync(async () =>
            {
                var response = await _autorService.GetByIdAsync(id);
                return Ok(response);
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AutorViewModel>))]
        public async Task<IActionResult> GetAll()
        {
            return await ExecuteAsync(async () =>
            {
                var response = await _autorService.GetAllAsync();
                return Ok(response);
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutorViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDomainValidation))]
        public async Task<IActionResult> Post([FromBody] AutorDto request)
        {
            return await ExecuteWithTransactionAsync(async () =>
            {
                var response = await _autorService.CreateAsync(request);
                return Ok(response);
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutorViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDomainValidation))]
        public async Task<IActionResult> Put(Guid id, [FromBody] AutorDto request)
        {
            return await ExecuteWithTransactionAsync(async () =>
            {
                var response = await _autorService.UpdateAsync(id, request);
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
                await _autorService.DeleteAsync(id);
                return Ok();
            });
        }
    }
}
