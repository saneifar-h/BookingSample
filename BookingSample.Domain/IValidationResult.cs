using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSample.Domain
{
    public interface IValidationResult
    {
        bool IsValid { get; }
        IEnumerable<string> Errors { get; }
        string ErrorString { get; }
    }

    public class ValidationResult : IValidationResult
    {
        private readonly List<string> _errors;

        public ValidationResult()
        {
            _errors = new List<string>();
        }

        public bool IsValid => !Errors.Any();

        public IEnumerable<string> Errors => _errors;
        public string ErrorString => string.Join(Environment.NewLine, Errors);

        public void Add(string error)
        {
            _errors.Add(error);
        }
    }
}