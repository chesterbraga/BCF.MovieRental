﻿using BCF.MovieRental.Business.Enums;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using BCF.MovieRental.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace BCF.MovieRental.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage, MessageType.Error);
            }
        }

        protected void Notify(string message, MessageType type = MessageType.Error)
        {
            _notifier.Handle(new Notification(message, type));
        }

        protected bool IsValid<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}