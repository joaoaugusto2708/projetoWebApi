﻿using System.ComponentModel.DataAnnotations;

namespace projetoWebApi.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
                return new ValidationResult("A primeira letra do nome do Produto deve ser maiúscula");

            return ValidationResult.Success;
        }
    }
}
