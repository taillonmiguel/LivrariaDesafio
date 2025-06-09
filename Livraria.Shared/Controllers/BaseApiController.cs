using Livraria.Shared.CustomException;
using Livraria.Shared.Data;
using Livraria.Shared.DomainValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.Shared.Controllers
{

    public abstract class BaseApiController<TLoggerClass>(IDomainValidation domainValidation, IUnityOfWork unityOfWork) : ControllerBase
    {
        private readonly IDomainValidation _domainValidation = domainValidation;
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        protected async Task<IActionResult> ExecuteAsync<TException>(Func<Task<IActionResult>> expression, Func<TException, Task<IActionResult>>? customExceptionHandling = null, Func<Task>? onSuccess = null) where TException : Exception
        {
            try
            {
                var response = await expression();

                if (!_domainValidation.Any())
                {
                    if (onSuccess != null)
                        await onSuccess();

                    return response;
                }

                return this.BadRequestBase(_domainValidation);
            }
            catch (Exception ex)
            {
                return await HandlingException(ex, customExceptionHandling);
            }
        }

        protected async Task<IActionResult> ExecuteWithTransactionAsync<TException>(Func<Task<IActionResult>> expression, Func<TException, Task<IActionResult>>? customExceptionHandling = null, Func<Task>? onSuccess = null) where TException : Exception
        {
            try
            {
                await _unityOfWork.BeginTransactionAsync();

                var response = await expression();

                if (!_domainValidation.Any())
                {
                    await _unityOfWork.SaveChangesAndCommitTransactionAsync();

                    if (onSuccess != null)
                        await onSuccess();

                    return response;
                }

                await _unityOfWork.RollbackTransactionAsync();

                return this.BadRequestBase(_domainValidation);
            }
            catch (Exception ex)
            {
                await _unityOfWork.RollbackTransactionAsync();

                return await HandlingException(ex, customExceptionHandling);
            }
        }

        protected async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> expression, Func<Task>? onSuccess = null)
        {
            return await ExecuteAsync<Exception>(expression, onSuccess: onSuccess);
        }

        protected async Task<IActionResult> ExecuteWithTransactionAsync(Func<Task<IActionResult>> expression, Func<Task>? onSuccess = null)
        {
            return await ExecuteWithTransactionAsync<Exception>(expression, onSuccess: onSuccess);
        }

        private async Task<IActionResult> HandlingException<TException>(Exception ex, Func<TException, Task<IActionResult>>? customExceptionHandling = null) where TException : Exception
        {
            if (ex is DomainValidationException)
            {
                return this.BadRequestBase(_domainValidation);
            }

            var exceptionService = HttpContext.RequestServices.GetService<ICustomExceptionHandler>();

            if (exceptionService != null)
                await exceptionService.Handler(ex);

            if (customExceptionHandling != null)
            {
                foreach (var param in customExceptionHandling.Method.GetParameters())
                {
                    if (param.ParameterType == ex.GetType())
                    {
                        return await customExceptionHandling((TException)ex);
                    }
                }
            }

            return this.BadRequestBase(_domainValidation, ex);
        }
    }
}