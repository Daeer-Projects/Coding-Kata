using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace DataMungingCoreV2.Helpers
{
    public static class Component
    {
        public static ValidationResult IsValid<T>(T component, AbstractValidator<T> validator)
        {
            var result = validator.Validate(component);

            return result;
        }
    }
}
