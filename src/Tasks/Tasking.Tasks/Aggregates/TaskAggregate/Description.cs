using Tasking.Shared;

namespace Tasking.Tasks.Aggregates.TaskAggregate
{
    public sealed class Description : ValueObject
    {
        public string Value { get; }

        public const int MinLength = 10;
        public const int MaxLength = 155;

        private Description(string value)
            => Value = value;

        public static Description Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < MinLength)
                throw Errors.TaskAggregateErrors.ShortDescription;

            if (value.Length > MaxLength)
                throw Errors.TaskAggregateErrors.LongDescription;

            var description = new Description(value);

            return description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
