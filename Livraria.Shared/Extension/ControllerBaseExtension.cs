using Livraria.Shared.DomainValidation;
using System.Net;

namespace Microsoft.AspNetCore.Mvc;

public static class ControllerBaseExtension
{
    public static IActionResult BadRequestBase(this ControllerBase controllerBase, IDomainValidation validation)
    {
        var _validation = (DomainValidation)validation;

        return controllerBase.BadRequest(new
        {
            message = string.IsNullOrEmpty(_validation.Message) ? "Dados inválidos" : _validation.Message,
            errors = _validation.FieldErrors,
            domainErrors = _validation.DomainErrors,
            validation = _validation.Validation
        });
    }

    public static IActionResult BadRequestBase(this ControllerBase controllerBase, IDomainValidation validation, Exception ex)
    {
        var _validation = (DomainValidation)validation;
        return controllerBase.StatusCode((int)HttpStatusCode.InternalServerError, new
        {
            message = "Erros não mapeados",
            errors = _validation.FieldErrors,
            domainErrors = _validation.DomainErrors,
            exception = ex.Message,
            stacktrace = ex.ToString(),
            validation = false
        });
    }
}