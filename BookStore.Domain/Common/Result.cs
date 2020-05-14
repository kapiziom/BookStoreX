using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace BookStore.Domain.Common
{
    public class Result : Result<object>
    {
        public Result() : base(null) { }
    }

    public class Result<T> where T : class
    {
        public Result(ValidationResult validationResult)
        {
            Errors = validationResult?.Errors.Select(x => x.ErrorMessage).ToList() ?? new List<string>();
        }

        [JsonIgnore]
        public T Value { get; set; }
        public IList<string> Errors { get; set; }
        public bool Succeeded { get { return Errors.Count == 0; } }

    }
}
