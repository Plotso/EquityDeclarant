namespace EquityDeclarant.Models.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class StatementInputModel : IValidatableObject
    {
        private const string CsvExtension = ".csv";
        
        [Required]
        public IFormFile Statement { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Statement != null && !Statement.FileName.EndsWith(CsvExtension))
            {
                yield return new ValidationResult("Only '.csv' files are supported!");
            }
        }
    }
}