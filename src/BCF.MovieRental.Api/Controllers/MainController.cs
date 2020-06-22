using System.Linq;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Notifications;
using BCF.MovieRental.Business.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BCF.MovieRental.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasErrors();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result,
                    alerts = _notifier.GetNotifications().Where(p => p.Type == MessageType.Alert).Select(n => n.Message)
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Where(p => p.Type == MessageType.Error).Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifierErroModelInvalid(modelState);
            return CustomResponse();
        }

        protected void NotifierErroModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}